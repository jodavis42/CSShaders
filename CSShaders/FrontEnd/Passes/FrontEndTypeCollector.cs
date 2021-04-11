using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Pass to collect types from the syntax tree. Collecting types is typically the first pass so that subsequent passes can use any type.
  /// </summary>
  class FrontEndTypeCollector : FrontEndPass
  {
    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
      if(!IsValidClassDeclaration(node))
        AddError(node, "Keyword 'class' not allowed. Use 'struct' instead");
    }

    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
      var structSymbol = GetDeclaredSymbol(node) as INamedTypeSymbol;
      ShaderType shaderType = mFrontEnd.mCurrentLibrary.FindType(new TypeKey(structSymbol));
      // Primitive types already handled
      if (shaderType != null)
      {
        if (shaderType.IsPrimitiveType())
          return;
        throw new Exception("Non primitive shader type already existed");
      }

      var attributes = mFrontEnd.ParseAttributes(structSymbol);

      shaderType = CreateShaderType(structSymbol, attributes);
      if (attributes.Contains(typeof(Shader.Vertex)))
        shaderType.mFragmentType = FragmentType.Vertex;
      else if (attributes.Contains(typeof(Shader.Pixel)))
        shaderType.mFragmentType = FragmentType.Pixel;

      shaderType.mMeta.mAttributes = attributes;
      ExtractDebugInfo(shaderType, structSymbol, node);
    }

    public ShaderType CreateShaderType(INamedTypeSymbol typeSymbol, ShaderAttributes attributes)
    {
      ShaderType shaderType = null;
      // If there's a special resolver for this type then use that to get the shader type
      foreach (var attribute in typeSymbol.GetAttributes())
      {
        var attributeName = TypeAliases.GetFullTypeName(attribute.AttributeClass);
        var processor = SpecialResolvers.SpecialTypeCreationAttributeProcessors.GetValueOrDefault(attributeName);
        shaderType = processor?.Invoke(mFrontEnd, typeSymbol, attribute);
        if (shaderType != null)
          return shaderType;
      }
      return CreateType(typeSymbol, OpType.Struct);
    }

    public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
    {
      var enumSymbol = GetDeclaredSymbol(node) as INamedTypeSymbol;
      
      var intType = FindType(typeof(int));
      // Load the underlying type of the enum
      var baseType = mFrontEnd.mCurrentLibrary.FindType(new TypeKey(enumSymbol.EnumUnderlyingType));
      // Currently only integers are supported for enums. @JoshD: To do
      if(intType != baseType)
      {
        var msg = string.Format("Enum {0} has a base type of {1}. Enums currently only support a base type of integer.", enumSymbol.Name, enumSymbol.EnumUnderlyingType.Name);
        mFrontEnd.CurrentProject.SendTranslationError(new ShaderCodeLocation(node.GetLocation()), msg);
        baseType = intType;
      }

      var attributes = mFrontEnd.ParseAttributes(enumSymbol);
      // Clone the integer's type data (width and signedness)
      var shaderType = CreateShaderType(enumSymbol, attributes);
      shaderType.mBaseType = baseType.mBaseType;
      shaderType.mParameters.Add(baseType.mParameters[0]);
      shaderType.mParameters.Add(baseType.mParameters[1]);

      shaderType.mMeta.mAttributes = attributes;
      ExtractDebugInfo(shaderType, enumSymbol, node);
    }
  }
}
