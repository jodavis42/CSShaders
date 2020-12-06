using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  ///  Collects declarations on all types. This collects the declared fields and functions, but none of their bodies or initializations.
  ///  This is so that any function or field can reference any other type's function or field safely.
  /// </summary>
  class FrontEndDeclarationCollector : FrontEndPass
  {
    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
      var shaderType = FindDeclaredType(node);
      // Don't process primitives (this is handled by a different pass)
      if (shaderType.IsPrimitiveType())
        return;
      
      mContext.mCurrentType = shaderType;
      
      var namedTypeSymbol = GetDeclaredSymbol(node) as INamedTypeSymbol;

      // Generate any preconstructor and default constructor functions if needed
      GeneratePreConstructor(namedTypeSymbol, mContext.mCurrentType);
      GenerateDefaultConstructor(namedTypeSymbol, mContext.mCurrentType);
      // Walk all children (functions and fields)
      base.VisitStructDeclaration(node);

      mContext.mCurrentType = null;
    }

    public void GeneratePreConstructor(INamedTypeSymbol structSymbol, ShaderType owningType)
    {
      // The preconstructor is an instance function that returns void and takes no args
      var preconstructorName = "PreConstructor";
      var returnType = FindType(typeof(void));
      var thisType = owningType.FindPointerType(StorageClass.Function);
      owningType.mPreConstructor = mFrontEnd.CreateFunctionAndType(owningType, returnType, preconstructorName, thisType, null);
    }

    private bool HasDefaultConstructor(INamedTypeSymbol structSymbol)
    {
      foreach (var constructor in structSymbol.Constructors)
      {
        if (constructor.IsImplicitlyDeclared)
        {
          return true;
        }
      }
      return false;
    }

    public void GenerateDefaultConstructor(INamedTypeSymbol structSymbol, ShaderType owningType)
    {
      // Only generate a default constructor if needed
      if (!HasDefaultConstructor(structSymbol))
        return;

      var constructorName = "DefaultConstructor";
      var returnType = FindType(typeof(void));
      var thisType = owningType.FindPointerType(StorageClass.Function);
      owningType.mImplicitConstructor = mFrontEnd.CreateFunctionAndType(owningType, returnType, constructorName, thisType, null); 
    }

    public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
      var returnType = FindType(typeof(void));
      var functionName = "Constructor";
      var shaderConstructor = CreateFunction(node, functionName, returnType);
      shaderConstructor.DebugInfo.Name = mFrontEnd.GenerateDebugFunctionName(mContext.mCurrentType, functionName);
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
      // Don't process intrinsics
      var attributes = mFrontEnd.ParseAttributes(GetDeclaredSymbol(node));
      if (IsIntrinsic(attributes))
        return;

      var functionName = node.Identifier.Text;
      var returnType = FindType(node.ReturnType);
      CreateFunction(node, functionName, returnType);
    }

    public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
      var owningType = mContext.mCurrentType;
      var shaderFieldType = FindType(node.Declaration.Type);

      // Fields can declare multiple variables. Create one shader field per variable declared.
      foreach (var variable in node.Declaration.Variables)
      {
        var variableSymbol = GetDeclaredSymbol(variable);
        if (variableSymbol.IsStatic)
        {
          var shaderField = mFrontEnd.CreateStaticField(owningType, shaderFieldType, variable.Identifier.Text, null);
          ParseAttributes(shaderField.mMeta, variableSymbol);
          ExtractDebugInfo(shaderField.InstanceOp, variableSymbol, variable);
        }
        else
        {
          var shaderField = mFrontEnd.CreateField(owningType, shaderFieldType, variable.Identifier.Text, null);
          ParseAttributes(shaderField.mMeta, variableSymbol);
          ExtractDebugInfo(shaderField, variableSymbol, variable);
        }
      }
    }
  }
}

