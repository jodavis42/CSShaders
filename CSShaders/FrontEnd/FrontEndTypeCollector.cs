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
        var processor = SpecialResolvers.SpecialTypeCreationAttributeProcessors.GetValueOrDefault(attribute.AttributeClass.Name);
        shaderType = processor?.Invoke(mFrontEnd, typeSymbol, attribute);
        if (shaderType != null)
          return shaderType;
      }
      return CreateType(typeSymbol, OpType.Struct);
    }
  }
}
