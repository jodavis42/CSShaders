﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Pass to collect and build primitive types. Primitive types can have dependencies on other primitive types at a time
  /// where multiple passes can't be used to solve our problem. Instead since we have a limited number of primitive types, 
  /// we can just processes them in a guaranteed resolvable order.
  /// </summary>
  class FrontEndPrimitiveTypeCollector : FrontEndPass
  {
    Dictionary<string, int> PrimitiveAttributePriorities = new Dictionary<string, int>()
    {
      { TypeAliases.GetFullTypeName<Math.IntegerPrimitive>(), 0 },
      { TypeAliases.GetFullTypeName<Math.FloatPrimitive>(), 0 },
      { TypeAliases.GetFullTypeName<Math.VectorPrimitive>(), 1 },
      // Matrices depend on vectors
      { TypeAliases.GetFullTypeName<Math.MatrixPrimitive>(), 2 },
      { TypeAliases.GetFullTypeName<Shader.SamplerPrimitive>(), 3 },
      { TypeAliases.GetFullTypeName<Shader.ImagePrimitive>(), 4 },
      { TypeAliases.GetFullTypeName<Shader.SampledImagePrimitive>(), 5 },
    };
    public class PrimitiveData
    {
      public int Priority = 0;
      public StructDeclarationSyntax Node;
      public INamedTypeSymbol StructSymbol;
    }

    List<PrimitiveData> PrimitivesToProcess = new List<PrimitiveData>();

    public override void VisitTrees(CSharpCompilation compilation, List<SyntaxTree> trees)
    {
      // Collect all primitives
      base.VisitTrees(compilation, trees);
      // Once we have all primitives we can now sort them and process them in the appropriate order
      ProcessDelayedPrimitives();
      // Clear to prevent this from bleeding into any later runs
      PrimitivesToProcess.Clear();
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
      if (!IsValidClassDeclaration(node))
        AddError(node, "Keyword 'class' not allowed. Use 'struct' instead");
    }

    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
      var structSymbol = GetDeclaredSymbol(node) as INamedTypeSymbol;
      var attributes = mFrontEnd.ParseAttributes(structSymbol);
      // If the type has a special primitive attribute then queue it up for processing
      foreach(var attribute in structSymbol.GetAttributes())
      {
        int priority = 0; 
        var attributeName = TypeAliases.GetFullTypeName(attribute.AttributeClass);
        if (PrimitiveAttributePriorities.TryGetValue(attributeName, out priority))
        {
          var data = new PrimitiveData();
          data.Priority = priority;
          data.Node = node;
          data.StructSymbol = structSymbol;
          PrimitivesToProcess.Add(data);
        }
      }
    }

    void ProcessDelayedPrimitives()
    {
      PrimitivesToProcess.Sort((lhs, rhs) => lhs.Priority.CompareTo(rhs.Priority));
      foreach (var primitiveData in PrimitivesToProcess)
      {
        var structSymbol = primitiveData.StructSymbol;
        var attributes = mFrontEnd.ParseAttributes(structSymbol);
        var shaderType = CreatePrimitiveShaderType(structSymbol, attributes);
        shaderType.mMeta.mAttributes = attributes;
        ExtractDebugInfo(shaderType, structSymbol, primitiveData.Node);
      }
    }

    public ShaderType CreatePrimitiveShaderType(INamedTypeSymbol typeSymbol, ShaderAttributes attributes)
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
      throw new Exception("Failed to create primitive");
    }
  }
}
