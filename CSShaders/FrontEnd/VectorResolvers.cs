using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Special resolvers for vector intrinsic types
  /// </summary>
  class VectorResolvers
  {
    public static ShaderType ProcessVectorType(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, AttributeData attribute)
    {
      var componentType = translator.FindType(new TypeKey(attribute.ConstructorArguments[0].Value as ISymbol));
      var componentCount = (UInt32)attribute.ConstructorArguments[1].Value;

      var shaderType = translator.CreateType(typeSymbol, OpType.Vector);
      shaderType.mParameters.Add(componentType);
      shaderType.mParameters.Add(translator.CreateConstantLiteral(componentCount));

      ProcessVectorDefaultConstructor(translator, typeSymbol, shaderType);

      return shaderType;
    }

    public static void ProcessVectorDefaultConstructor(FrontEndTranslator translator, INamedTypeSymbol typeSymbol, ShaderType shaderType)
    {
      ShaderLibrary.InstrinsicDelegate defaultConstructorResolver = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
      {
        ResolveVectorDefaultConstructor(translator, context, shaderType);
      };
      foreach (var constructor in typeSymbol.Constructors)
      {
        if (constructor.Parameters.Length == 0 && constructor.IsImplicitlyDeclared)
          translator.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(constructor), defaultConstructorResolver);
      }
    }

    public static void ResolveVectorDefaultConstructor(FrontEndTranslator translator, FrontEndContext context, ShaderType vectorType)
    {
      var componentType = vectorType.mParameters[0] as ShaderType;
      var componentCount = (UInt32)(vectorType.mParameters[1] as ShaderConstantLiteral).mValue;

      var zeroConstantLiteral = translator.CreateConstantLiteral(componentType, "0");
      var zeroConstant = translator.CreateConstantOp(componentType, zeroConstantLiteral);
      
      var constructorArgs = new List<IShaderIR>();
      for (var i = 0; i < componentCount; ++i)
        constructorArgs.Add(zeroConstant);
      var op = translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpCompositeConstruct, vectorType, constructorArgs);
      context.Push(op);
    }

    public static void ProcessVectorSwizzle(FrontEndTranslator frontEnd, INamedTypeSymbol typeSymbol, ISymbol memberSymbol, AttributeData attribute)
    {
      if (memberSymbol is IFieldSymbol fieldSymbol)
      {
        ShaderLibrary.InstrinsicDelegate getterResolver = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
        {
          ResolveVectorSwizzleGetter(translator, context, attribute, fieldSymbol.Type, arguments);
        };
        ShaderLibrary.InstrinsicSetterDelegate setterResolver = (FrontEndTranslator translator, IShaderIR selfInstance, IShaderIR rhsIR, FrontEndContext context) =>
        {
          ResolveVectorSwizzleSetter(translator, context, attribute, fieldSymbol.Type, selfInstance, rhsIR);
        };
        frontEnd.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(memberSymbol), getterResolver);
        frontEnd.mCurrentLibrary.CreateIntrinsicSetterFunction(new FunctionKey(fieldSymbol), setterResolver);
      }
      if (memberSymbol is IPropertySymbol propertySymbol)
      {
        ShaderLibrary.InstrinsicDelegate getterResolver = (FrontEndTranslator translator, List<IShaderIR> arguments, FrontEndContext context) =>
        {
          ResolveVectorSwizzleGetter(translator, context, attribute, propertySymbol.Type, arguments);
        };
        ShaderLibrary.InstrinsicSetterDelegate setterResolver = (FrontEndTranslator translator, IShaderIR selfInstance, IShaderIR rhsIR, FrontEndContext context) =>
        {
          ResolveVectorSwizzleSetter(translator, context, attribute, propertySymbol.Type, selfInstance, rhsIR);
        };
        if (propertySymbol.GetMethod != null)
          frontEnd.mCurrentLibrary.CreateIntrinsicFunction(new FunctionKey(propertySymbol.GetMethod), getterResolver);
        if (propertySymbol.SetMethod != null)
          frontEnd.mCurrentLibrary.CreateIntrinsicSetterFunction(new FunctionKey(propertySymbol.SetMethod), setterResolver);
      }
    }

    public static void ResolveVectorSwizzleGetter(FrontEndTranslator translator, FrontEndContext context, AttributeData attribute, ISymbol returnType, List<IShaderIR> arguments)
    {
      var swizzleElements = attribute.ConstructorArguments[0].Values;
      var self = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, arguments[0]);
      var resultType = translator.mCurrentLibrary.FindType(new TypeKey(returnType));
      
      // By default, assume the swizzle is 1 element (just a composite extract)
      var swizzleArgs = new List<IShaderIR>() { self };
      var opType = OpInstructionType.OpCompositeExtract;
      // If it's multiple elements, change this to a vector shuffle
      if (swizzleElements.Length != 1)
      { 
        swizzleArgs.Add(self);
        opType = OpInstructionType.OpVectorShuffle;
      }
      // Add all elements to grab for the swizzle from the attribute
      foreach (var element in swizzleElements)
        swizzleArgs.Add(translator.CreateConstantLiteral((UInt32)element.Value));
      
      var op = translator.CreateOp(context.mCurrentBlock, opType, resultType, swizzleArgs);
      context.Push(op);
    }

    public static void ResolveVectorSwizzleSetter(FrontEndTranslator translator, FrontEndContext context, AttributeData attribute, ISymbol returnType, IShaderIR selfInstance, IShaderIR rhsIR)
    {
      var swizzleElements = attribute.ConstructorArguments[0].Values;
      var selfValue = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, selfInstance);
      var selfValueType = selfValue.mResultType.GetDereferenceType();
      var componentCount = (UInt32)(selfValueType.mParameters[1] as ShaderConstantLiteral).mValue;

      var rhs = translator.GetOrGenerateValueTypeFromIR(context.mCurrentBlock, rhsIR);

      // If we're setting just a single element, then do an access chain and to set that element
      if (swizzleElements.Length == 1)
      {
        // Find what element we're setting
        var constantLiteral = translator.CreateConstantLiteral((UInt32)swizzleElements[0].Value);
        var memberIndexConstant = translator.CreateConstantOp(translator.FindType(typeof(uint)), constantLiteral);
        // Lookup the result type
        var resultType = translator.mCurrentLibrary.FindType(new TypeKey(returnType));
        resultType = resultType.FindPointerType(selfValue.mResultType.mStorageClass);
        // Build the access chain to the element
        var memberVariableOp = translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpAccessChain, resultType, new List<IShaderIR> { selfInstance, memberIndexConstant });

        // Then set this back to the lhs side
        translator.CreateStoreOp(context.mCurrentBlock, memberVariableOp, rhs);
      }
      // Otherwise construct a new vector and set it over
      else
      {
        var swizzleArgs = new List<IShaderIR>() { selfValue, rhs };

        // Build up a set of what element indices were in the swizzle
        var elementSet = new HashSet<UInt32>();
        foreach (var element in swizzleElements)
        {
          elementSet.Add((UInt32)element.Value);
        }

        // Foreach element in the new vector, choose if we take it from the rhs or the lhs. If the element index is in the swizzle ops,
        // then take the next element from rhs, otherwise take the same index from lhs.
        var rhsElementIndex = 0u;
        for (uint element = 0; element < componentCount; ++element)
        {
          if (elementSet.Contains(element))
          {
            swizzleArgs.Add(translator.CreateConstantLiteral(componentCount + rhsElementIndex));
            ++rhsElementIndex;
          }
          else
            swizzleArgs.Add(translator.CreateConstantLiteral(element));
        }
        // To build the setter, first create the new vector from components in the lhs/rhs as appropriate.
        var op = translator.CreateOp(context.mCurrentBlock, OpInstructionType.OpVectorShuffle, selfValueType, swizzleArgs);
        // Then set this back to the lhs side
        translator.CreateStoreOp(context.mCurrentBlock, selfInstance, op);
      }
    }
  }
}
