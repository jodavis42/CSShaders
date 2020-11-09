using System.Collections.Generic;

namespace CSShaders
{
  /// <summary>
  /// A collection of all storage class variants of a type. In SpirV, there's the basic 'value' type for any type, 
  /// but then there are several different qualifiers for pointer types (Function, Private, etc...)
  /// </summary>
  public class ShaderTypeStorageClassCollection
  {
    public Dictionary<StorageClass, ShaderType> mPointerStorageClasses = new Dictionary<StorageClass, ShaderType>();
    public ShaderType mDereferenceType;

    public ShaderTypeStorageClassCollection(ShaderType dereferenceType)
    {
      mDereferenceType = dereferenceType;
    }

    public void AddPointerType(StorageClass storageClass, ShaderType shaderPointerType)
    {
      mPointerStorageClasses.Add(storageClass, shaderPointerType);
      shaderPointerType.StorageClassCollection = this;
    }

    public ShaderType FindPointerType(StorageClass storageClass)
    {
      return mPointerStorageClasses.GetValueOrDefault(storageClass);
    }

    public ShaderType GetDereferenceType()
    {
      return mDereferenceType;
    }
  }
}
