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

    public ParameterDescription()
    {

    }

    public ParameterDescription(TypeName typeName, string parameterName)
    {
      TypeName = typeName;
      ParameterName = parameterName;
    }

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
    public List<string> Comments = null;
    public IAttributesGenerator Attributes = null;
    public string Qualifiers = "public extern static";
    public TypeName ReturnTypeName = null;
    public List<ParameterDescription> Parameters = new List<ParameterDescription>();

    public IntrinsicSignature AddOverrideComment(string comment)
    {
      if (Comments == null)
        Comments = new List<string>();
      Comments.Add(comment);
      return this;
    }

    public IntrinsicSignature SetAttributes(IAttributesGenerator attributes)
    {
      Attributes = attributes;
      return this;
    }

    public IntrinsicSignature SetQualifiers(string qualifiers)
    {
      Qualifiers = qualifiers;
      return this;
    }
  }

  /// <summary>
  /// The description for an intrinsic. An intrinsic is a special function in shaders.
  /// Each intrinsic can have several overloads for different types, but they all have the same core set of attributes.
  /// </summary>
  public class IntrinsicDescription
  {
    public bool Disabled = false;
    public string IntrinsicName;
    public List<string> Comments = new List<string>();
    public IAttributesGenerator Attributes;
    public string FunctionName;
    public List<IntrinsicSignature> Signatures = new List<IntrinsicSignature>();
    /// The name of the test when emitting tests. If this is null, the intrinsic name is used.
    public string TestName = null;

    public IntrinsicSignature AddSignature(IAttributesGenerator attributes, TypeName returnType, TypeName param0Type, string param0Name)
    {
      var signature = new IntrinsicSignature() { Attributes = attributes, ReturnTypeName = returnType };
      signature.Parameters.Add(new ParameterDescription { TypeName = param0Type, ParameterName = param0Name });
      Signatures.Add(signature);
      return signature;
    }

    public IntrinsicSignature AddSignature(TypeName returnType, TypeName param0Type, string param0Name)
    {
      return AddSignature(null, returnType, param0Type, param0Name);
    }

    public IntrinsicDescription AddSignatures(List<TypeName> returnTypes, List<TypeName> param0Types, string param0Name)
    {
      for (var i = 0; i < param0Types.Count; ++i)
        AddSignature(returnTypes[i], param0Types[i], param0Name);
      return this;
    }

    public IntrinsicSignature AddSignature(TypeName returnType, TypeName param0Type, string param0Name, TypeName param1Type, string param1Name)
    {
      var signature = new IntrinsicSignature() { ReturnTypeName = returnType };
      signature.Parameters.Add(new ParameterDescription { TypeName = param0Type, ParameterName = param0Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param1Type, ParameterName = param1Name });
      Signatures.Add(signature);
      return signature;
    }

    public IntrinsicDescription AddSignatures(List<TypeName> returnTypes, List<TypeName> param0Types, string param0Name, List<TypeName> param1Types, string param1Name)
    {
      for (var i = 0; i < param0Types.Count; ++i)
        AddSignature(returnTypes[i], param0Types[i], param0Name, param1Types[i], param1Name);
      return this;
    }

    public IntrinsicSignature AddSignature(TypeName returnType, TypeName param0Type, string param0Name, TypeName param1Type, string param1Name, TypeName param2Type, string param2Name)
    {
      var signature = new IntrinsicSignature() { ReturnTypeName = returnType };
      signature.Parameters.Add(new ParameterDescription { TypeName = param0Type, ParameterName = param0Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param1Type, ParameterName = param1Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param2Type, ParameterName = param2Name });
      Signatures.Add(signature);
      return signature;
    }

    public IntrinsicSignature AddSignature(TypeName returnType, TypeName param0Type, string param0Name, TypeName param1Type, string param1Name, TypeName param2Type, string param2Name, TypeName param3Type, string param3Name)
    {
      var signature = new IntrinsicSignature() { ReturnTypeName = returnType };
      signature.Parameters.Add(new ParameterDescription { TypeName = param0Type, ParameterName = param0Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param1Type, ParameterName = param1Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param2Type, ParameterName = param2Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param3Type, ParameterName = param3Name });
      Signatures.Add(signature);
      return signature;
    }

    public IntrinsicSignature AddSignature(TypeName returnType,
      TypeName param0Type, string param0Name,
      TypeName param1Type, string param1Name,
      TypeName param2Type, string param2Name,
      TypeName param3Type, string param3Name,
      TypeName param4Type, string param4Name)
    {
      var signature = new IntrinsicSignature() { ReturnTypeName = returnType };
      signature.Parameters.Add(new ParameterDescription { TypeName = param0Type, ParameterName = param0Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param1Type, ParameterName = param1Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param2Type, ParameterName = param2Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param3Type, ParameterName = param3Name });
      signature.Parameters.Add(new ParameterDescription { TypeName = param4Type, ParameterName = param4Name });
      Signatures.Add(signature);
      return signature;
    }

    public IntrinsicSignature AddSignature(TypeName returnType, List<ParameterDescription> parameters)
    {
      var signature = new IntrinsicSignature() { ReturnTypeName = returnType, Parameters = parameters};
      Signatures.Add(signature);
      return signature;
    }

    public IntrinsicDescription AddSignatures(List<TypeName> returnTypes, List<TypeName> param0Types, string param0Name, List<TypeName> param1Types, string param1Name, List<TypeName> param2Types, string param2Name)
    {
      for (var i = 0; i < param0Types.Count; ++i)
        AddSignature(returnTypes[i], param0Types[i], param0Name, param1Types[i], param1Name, param2Types[i], param2Name);
      return this;
    }

    public IntrinsicDescription AddIntrinsicComment(string comment)
    {
      Comments.Add(comment);
      return this;
    }
  }
}
