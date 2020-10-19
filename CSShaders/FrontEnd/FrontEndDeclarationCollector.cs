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
      // Set the current type and then recurse
      mContext.mCurrentType = FindDeclaredType(node);
      GeneratePreConstructor(GetDeclaredSymbol(node)as INamedTypeSymbol, mContext.mCurrentType);
      GenerateDefaultConstructor(GetDeclaredSymbol(node) as INamedTypeSymbol, mContext.mCurrentType);
      base.VisitStructDeclaration(node);
      mContext.mCurrentType = null;
    }

    public void GeneratePreConstructor(INamedTypeSymbol structSymbol, ShaderType owningType)
    {
      var returnType = FindType(typeof(void));
      var thisType = owningType.mTypeStorage.mPointerType;
      var preconstructorName = string.Format("{0}_{1}", "PreConstructor", owningType.DebugInfo.Name);
      
      var shaderFunctionType = mFrontEnd.CreateFunctionType(returnType, thisType, null);
      var shaderFunction = mFrontEnd.CreateFunction(owningType, returnType, preconstructorName, thisType, null);
      shaderFunction.mResultType = shaderFunctionType;
      owningType.mPreConstructor = shaderFunction;
    }

    public void GenerateDefaultConstructor(INamedTypeSymbol structSymbol, ShaderType owningType)
    {
      bool hasDefaultConstructor = false;
      foreach (var constructor in structSymbol.Constructors)
      {
        if(constructor.IsImplicitlyDeclared)
        {
          hasDefaultConstructor = true;
          break;
        }
      }
      if (!hasDefaultConstructor)
        return;

      var returnType = FindType(typeof(void));
      var thisType = owningType.mTypeStorage.mPointerType;
      var preconstructorName = string.Format("{0}_{1}", "DefaultConstructor", owningType.DebugInfo.Name);

      var shaderFunctionType = mFrontEnd.CreateFunctionType(returnType, thisType, null);
      var shaderFunction = mFrontEnd.CreateFunction(owningType, returnType, preconstructorName, thisType, null);
      shaderFunction.mResultType = shaderFunctionType;
      owningType.mImplicitConstructor = shaderFunction;
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
      var fnSymbol = mFrontEnd.mSemanticModel.GetDeclaredSymbol(node);
      var owningType = mContext.mCurrentType;

      // Collect function return and parameter types from the syntax node
      var returnType = FindType(node.ReturnType);
      var paramTypes = new List<ShaderType>();
      foreach (var param in node.ParameterList.Parameters)
      {
        var paramType = FindType(param.Type);
        paramTypes.Add(paramType);
      }
      var thisType = owningType.mTypeStorage.mPointerType;

      var shaderFunctionType = mFrontEnd.CreateFunctionType(returnType, thisType, paramTypes);
      var shaderFunction = mFrontEnd.CreateFunction(owningType, returnType, node.Identifier.Text, thisType, paramTypes);
      shaderFunction.mResultType = shaderFunctionType;

      shaderFunction.mMeta.mAttributes = mFrontEnd.ParseAttributes(fnSymbol);
      ExtractDebugInfo(shaderFunction, fnSymbol, node);
      mFrontEnd.mCurrentLibrary.AddFunction(new FunctionKey(fnSymbol), shaderFunction);
    }

    public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
      var owningType = mContext.mCurrentType;
      var shaderFieldType = FindType(node.Declaration.Type);

      // Fields can declare multiple variables. Create one shader field per variable declared.
      foreach (var variable in node.Declaration.Variables)
      {
        var fieldSymbol = GetDeclaredSymbol(variable);
        var shaderField = mFrontEnd.CreateField(owningType, shaderFieldType, variable.Identifier.ToString(), null);
        shaderField.mMeta.mAttributes = mFrontEnd.ParseAttributes(fieldSymbol);
        ExtractDebugInfo(shaderField, fieldSymbol, variable);
      }
    }

  }
}

