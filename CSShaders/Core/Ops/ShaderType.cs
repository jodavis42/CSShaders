using System.Collections.Generic;
using System.Diagnostics;

namespace CSShaders
{
  /// <summary>
  /// Denotes that a type is explicitly associated with a certain shader stage.
  /// </summary>
  public enum FragmentType
  {
    None, Vertex, Pixel
  }

  /// <summary>
  /// Represents a type in the shader language. Types are built up from other types recursively. Types also contain all relevenat fields and functions declared with this type.
  /// </summary>
  [DebuggerDisplay("{GetPrettyName()}")]
  public class ShaderType : IShaderIR
  {
    /// The underlying OpType of this type. Helps denote if this is a simple primitive type or something more complicated
    public OpType mBaseType = OpType.Void;
    /// Certain types must be declared with certain storage requirements like if they're used in functions or at global scope.
    public StorageClass mStorageClass = StorageClass.Function;
    /// If this is a non-primitive type, then the paramters are the sub-types this type is made up of.
    public List<IShaderIR> mParameters = new List<IShaderIR>();

    public List<ShaderFunction> mFunctions = new List<ShaderFunction>();
    public ShaderFunction mPreConstructor = null;
    public ShaderFunction mImplicitConstructor = null;
    public List<ShaderField> mFields = new List<ShaderField>();

    public ShaderTypeStorageClassCollection StorageClassCollection = null;

    /// What fragment shader type this type is.
    public FragmentType mFragmentType = FragmentType.None;
    public ShaderTypeMeta mMeta = new ShaderTypeMeta();

    public ShaderType FindPointerType(StorageClass storageClass)
    {
      return StorageClassCollection.FindPointerType(storageClass);
    }
    public ShaderType GetDereferenceType()
    {
      return StorageClassCollection.GetDereferenceType();
    }

    public static bool IsSignedInt(ShaderType shaderType)
    {
      if (shaderType.mParameters.Count >= 2)
      {
        var signednessLiteral = shaderType.mParameters[1] as ShaderConstantLiteral;
        return (uint)signednessLiteral.mValue == 1;
      }
      return true;
    }

    public string GetPrettyName()
    {
      // Determine if this is a signed or unsigned int
      if (mBaseType == OpType.Int)
      {
        var widthLiteral = mParameters[0] as ShaderConstantLiteral;
        var signedLiteral = mParameters[1] as ShaderConstantLiteral;
        return string.Format("{0}Int{1}", IsSignedInt(this) ? "U" : "", (uint)widthLiteral.mValue);

      }
      return mMeta.mName;
    }
  }
}
