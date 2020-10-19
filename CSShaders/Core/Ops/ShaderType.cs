using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// Denotes that a type is explicitly associated with a certain shader stage.
  /// </summary>
  public enum FragmentType
  {
    None, Vertex, Pixel
  }

  public class ShaderTypeStorage
  {
    public ShaderType mPointerType = null;
    public ShaderType mDereferenceType = null;
  }

  /// <summary>
  /// Represents a type in the shader language. Types are built up from other types recursively. Types also contain all relevenat fields and functions declared with this type.
  /// </summary>
  public class ShaderType : IShaderIR
  {
    /// The underlying OpType of this type. Helps denote if this is a simple primitive type or something more complicated
    public OpType mBaseType = OpType.Void;
    /// Certain types must be declared with certain storage requirements like if they're used in functions or at global scope.
    public StorageClass mStorageClass = StorageClass.Generic;
    /// If this is a non-primitive type, then the paramters are the sub-types this type is made up of.
    public List<IShaderIR> mParameters = new List<IShaderIR>();
    
    public List<ShaderFunction> mFunctions = new List<ShaderFunction>();
    public ShaderFunction mPreConstructor = null;
    public ShaderFunction mImplicitConstructor = null;
    public List<ShaderField> mFields = new List<ShaderField>();
    
    public ShaderTypeStorage mTypeStorage = null;

    /// What fragment shader type this type is.
    public FragmentType mFragmentType = FragmentType.None;
    public ShaderTypeMeta mMeta = new ShaderTypeMeta();
  }
}
