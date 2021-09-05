using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSShaders
{
  /// <summary>
  /// Denotes that a type is explicitly associated with a certain shader stage.
  /// </summary>
  public enum FragmentType
  {
    None = -1, Start = 0, Vertex = 0, Pixel, Count
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
    public List<GlobalShaderField> mStaticFields = new List<GlobalShaderField>();

    public ShaderTypeStorageClassCollection StorageClassCollection = null;

    /// What fragment shader type this type is.
    public FragmentType mFragmentType = FragmentType.None;
    public ShaderTypeMeta mMeta = new ShaderTypeMeta();

    public List<ShaderEntryPointInfo> mEntryPoints = new List<ShaderEntryPointInfo>();

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

    public static ShaderType FindLeafType(ShaderType shaderType)
    {
      if (shaderType.mBaseType == OpType.Vector)
        return shaderType.mParameters[0] as ShaderType;
      if (shaderType.mBaseType == OpType.Matrix)
        return FindLeafType(shaderType.mParameters[0] as ShaderType);
      return shaderType;
    }

    public string GetPrettyName()
    {
      // Determine if this is a signed or unsigned int
      if (mBaseType == OpType.Int)
      {
        var widthLiteral = mParameters[0] as ShaderConstantLiteral;
        var signedLiteral = mParameters[1] as ShaderConstantLiteral;
        return string.Format("{0}Int{1}", IsSignedInt(this) ? "" : "U", (uint)widthLiteral.mValue);
      }
      if (mBaseType == OpType.Pointer)
        return $"{mMeta.mName}*";
      return mMeta.mName;
    }

    public bool IsPrimitiveType()
    {
      return !(mBaseType == OpType.Struct || mBaseType == OpType.Pointer || mBaseType == OpType.Function);
    }

    public UInt32 ComputeByteSize()
    {
      if (mBaseType == OpType.Bool || mBaseType == OpType.Int || mBaseType == OpType.Float)
        return 4;
      if (mBaseType == OpType.Vector)
      {
        var vectorType = VectorType.Load(this);
        var componentType = vectorType.GetComponentType();
        var componentCount = vectorType.GetComponentCount();
        return componentCount * componentType.ComputeByteSize();
      }
      if (mBaseType == OpType.Matrix)
      {
        var matrixType = MatrixType.Load(this);
        var componentType = matrixType.GetComponentType();
        return matrixType.GetComponentCount() * componentType.ComputeByteAlignment();
      }
      if (mBaseType == OpType.Struct)
      {
        // Each element has to actually be properly aligned in order to get the
        // correct size though otherwise this can drift very wildly.
        // For example struct { float A; vec3 B; float C; vec3 D; }
        // Is actually size 16 + 16 + 16 + 12 = 60 due to vec3 having 
        // to be aligned on 16 byte boundaries.
        UInt32 byteSize = 0;
        foreach (var field in mFields)
        {
          var fieldType = field.mType;
          var memberAlignment = fieldType.ComputeByteAlignment();
          var memberByteSize = fieldType.ComputeByteSize();
          byteSize = ComputeSizeAfterAlignment(byteSize, memberAlignment);
          byteSize += memberByteSize;
        }

        // Vulkan Spec: A struct has a base alignment equal to the largest base alignment
        // of any of its members rounded up to a multiple of 16.
        return ComputeSizeAfterAlignment(byteSize, 16);
      }
      throw new Exception();
    }

    public UInt32 ComputeByteAlignment()
    {
      if (mBaseType == OpType.Bool || mBaseType == OpType.Int || mBaseType == OpType.Float)
        return 4;
      if (mBaseType == OpType.Vector)
      {
        var vectorType = VectorType.Load(this);
        var componentType = vectorType.GetComponentType();
        var componentCount = vectorType.GetComponentCount();
        // Vec3 has to be padded out to a Vec4
        if (componentCount == 3)
          componentCount = 4;
        return componentCount * componentType.ComputeByteAlignment();
      }
      if (mBaseType == OpType.Matrix)
      {
        // Via opengl/dx matrix types are treated as an array of the vector types where the vector
        // types are padded up to vec4s. This happens for efficiency reason (at least with uniform buffers).
        var matrixType = MatrixType.Load(this);
        var componentType = matrixType.GetComponentType();
        return 4 * componentType.ComputeByteAlignment();
      }
      if (mBaseType == OpType.Struct)
      {
        // The alignment of a struct is the max alignment of all of its members
        UInt32 alignment = 0;
        foreach (var field in mFields)
        {
          var fieldType = field.mType;
          alignment = System.Math.Max(alignment, fieldType.ComputeByteAlignment());
        }
        // Vulkan Spec: A struct has a base alignment equal to the largest base alignment
        // of any of its members rounded up to a multiple of 16.
        return ComputeSizeAfterAlignment(alignment, 16);
      }
      throw new Exception();
    }
    public int GetStride(float baseAlignment)
    {
      var typeSize = this.ComputeByteSize();
      int stride = (int)(baseAlignment * System.Math.Ceiling(typeSize / baseAlignment));
      return stride;
    }

    public static UInt32 ComputeSizeAfterAlignment(UInt32 byteSize, UInt32 baseAlignment)
    {
      // Get the remainder to add
      var remainder = baseAlignment - (byteSize % baseAlignment);
      // Mod with the required alignment to get offset 0 when needed
      var alignmentOffset = remainder % baseAlignment;
      byteSize += alignmentOffset;
      return byteSize;
    }
  }
}
