using System.Collections.Generic;

namespace IntrinsicsGenerators
{
  /// <summary>
  /// Describes a parameter for a function.
  /// </summary>
  public class ParameterDescription
  {
    public TypeName TypeName;
    public string ParameterName;

    public string GetDeclaration()
    {
      return TypeName + " " + ParameterName;
    }
  }

  /// <summary>
  /// One signature for an intrinsic. A signature contains the return type and all parameters.
  /// </summary>
  public class IntrinsicSignature
  {
    public TypeName ReturnTypeName;
    public List<ParameterDescription> Parameters = new List<ParameterDescription>();
  }

  /// <summary>
  /// The description for an intrinsic. An intrinsic is a special function in shaders.
  /// Each intrinsic can have several overloads for different types, but they all have the same core set of attributes.
  /// </summary>
  public class IntrinsicDescription
  {
    public List<string> Comments = new List<string>();
    public string Attributes;
    public string IntrinsicName;
    public string FunctionName;
    public List<IntrinsicSignature> Signatures = new List<IntrinsicSignature>();
    public bool Disabled = false;

    public IntrinsicDescription AddSignature(TypeName returnType, TypeName param0Type, string param0Name)
    {
      var signature = new IntrinsicSignature() { ReturnTypeName = returnType };
      signature.Parameters.Add(new ParameterDescription { TypeName = param0Type, ParameterName = param0Name });
      Signatures.Add(signature);
      return this;
    }

    public IntrinsicDescription AddSignatures(List<TypeName> returnTypes, List<TypeName> param0Types, string param0Name)
    {
      for (var i = 0; i < param0Types.Count; ++i)
        AddSignature(returnTypes[i], param0Types[i], param0Name);
      return this;
    }

    public IntrinsicDescription AddSignature(TypeName returnType, TypeName param0Type, string param0Name, TypeName param1Type, string param1Name)
    {
      var signature = new IntrinsicSignature() { ReturnTypeName = returnType };
      signature.Parameters.Add(new ParameterDescription { TypeName = param0Type, ParameterName = param0Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param1Type, ParameterName = param1Name });
      Signatures.Add(signature);
      return this;
    }

    public IntrinsicDescription AddSignatures(List<TypeName> returnTypes, List<TypeName> param0Types, string param0Name, List<TypeName> param1Types, string param1Name)
    {
      for (var i = 0; i < param0Types.Count; ++i)
        AddSignature(returnTypes[i], param0Types[i], param0Name, param1Types[i], param1Name);
      return this;
    }

    public IntrinsicDescription AddSignature(TypeName returnType, TypeName param0Type, string param0Name, TypeName param1Type, string param1Name, TypeName param2Type, string param2Name)
    {
      var signature = new IntrinsicSignature() { ReturnTypeName = returnType };
      signature.Parameters.Add(new ParameterDescription { TypeName = param0Type, ParameterName = param0Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param1Type, ParameterName = param1Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param2Type, ParameterName = param2Name });
      Signatures.Add(signature);
      return this;
    }

    public IntrinsicDescription AddSignatures(List<TypeName> returnTypes, List<TypeName> param0Types, string param0Name, List<TypeName> param1Types, string param1Name, List<TypeName> param2Types, string param2Name)
    {
      for (var i = 0; i < param0Types.Count; ++i)
        AddSignature(returnTypes[i], param0Types[i], param0Name, param1Types[i], param1Name, param2Types[i], param2Name);
      return this;
    }

    public IntrinsicDescription AddComment(string comment)
    {
      Comments.Add(comment);
      return this;
    }
  }
}
