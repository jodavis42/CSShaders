using System.Collections.Generic;

namespace IntrinsicsGenerators
{
  /// <summary>
  /// A group of intrinsics. Mostly used to batch together common intrinsics so they can be declared together
  /// </summary>
  public class IntrinsicGroup
  {
    /// <summary>
    /// The name of the group. Used whenever a intrinsics are grouped together somehow (such as the folder name for tests).
    /// </summary>
    public string GroupName;
    /// <summary>
    /// The comment used to signify the start of a group
    /// </summary>
    public string GroupComment;
    /// <summary>
    /// All of the intrinsics in this group.
    /// </summary>
    public List<IntrinsicDescription> Intrinsics = new List<IntrinsicDescription>();

    public IntrinsicDescription Build(string intrinsicName, string functionName, IAttributesGenerator attributes)
    {
      var intrinsic = new IntrinsicDescription()
      {
        IntrinsicName = intrinsicName,
        FunctionName = functionName,
        Attributes = attributes
      };
      Intrinsics.Add(intrinsic);
      return intrinsic;
    }

    public IntrinsicDescription Build(IntrinsicsFunctionDescription fnCoreDesc)
    {
      return Build(fnCoreDesc.GetIntrinsicName(), fnCoreDesc.GetFunctionName(), fnCoreDesc);
    }

    public IntrinsicDescription Build(string intrinsicName, string functionName)
    {
      return Build(intrinsicName, functionName, null);
    }

    public IntrinsicDescription Build(IntrinsicsFunctionDescription fnCoreDesc, List<TypeName> returnTypes, List<TypeName> param0Types)
    {
      return Build(fnCoreDesc, returnTypes, param0Types, "value");
    }

    public IntrinsicDescription Build(IntrinsicsFunctionDescription fnCoreDesc, List<TypeName> returnTypes,
      List<TypeName> param0Types, string param0Name)
    {
      var intrinsic = Build(fnCoreDesc);
      intrinsic.AddSignatures(returnTypes, param0Types, param0Name);
      return intrinsic;
    }

    public IntrinsicDescription Build(IntrinsicsFunctionDescription fnCoreDesc, List<TypeName> returnTypes,
      List<TypeName> param0Types, List<TypeName> param1Types)
    {
      return Build(fnCoreDesc, returnTypes, param0Types, "lhs", param1Types, "rhs");
    }

    public IntrinsicDescription Build(IntrinsicsFunctionDescription fnCoreDesc, List<TypeName> returnTypes,
      List<TypeName> param0Types, string param0Name, List<TypeName> param1Types, string param1Name)
    {
      var intrinsic = Build(fnCoreDesc);
      intrinsic.AddSignatures(returnTypes, param0Types, param0Name, param1Types, param1Name);
      return intrinsic;
    }

    public IntrinsicDescription Build(IntrinsicsFunctionDescription fnCoreDesc, List<TypeName> returnTypes,
      List<TypeName> param0Types, string param0Name, List<TypeName> param1Types, string param1Name, List<TypeName> param2Types, string param2Name)
    {
      var intrinsic = Build(fnCoreDesc);
      intrinsic.AddSignatures(returnTypes, param0Types, param0Name, param1Types, param1Name, param2Types, param2Name);
      return intrinsic;
    }
  }
}
