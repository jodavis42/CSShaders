using System;
using System.Collections.Generic;

namespace CSShaders
{
  class Decorations
  {
    public static void AddDecorationBlock(FrontEndTranslator translator, ShaderType shaderType, ShaderBlock decorationsBlock)
    {
      var decorationBlockLiteral = translator.CreateConstantLiteral((int)Spv.Decoration.DecorationBlock);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpDecorate, null, new List<IShaderIR>() { shaderType, decorationBlockLiteral });
    }

    public static void AddDecorationBinding(FrontEndTranslator translator, IShaderIR ir, int bindingId, ShaderBlock decorationsBlock)
    {
      var decorationBindingLiteral = translator.CreateConstantLiteral((int)Spv.Decoration.DecorationBinding);
      var bindingIdLiteral = translator.CreateConstantLiteral(bindingId);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpDecorate, null, new List<IShaderIR>() { ir, decorationBindingLiteral, bindingIdLiteral });
    }

    public static void AddDecorationDescriptorSet(FrontEndTranslator translator, IShaderIR ir, int descriptorSetId, ShaderBlock decorationsBlock)
    {
      var decorationBindingLiteral = translator.CreateConstantLiteral((int)Spv.Decoration.DecorationDescriptorSet);
      var descriptorSetIdLiteral = translator.CreateConstantLiteral(descriptorSetId);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpDecorate, null, new List<IShaderIR>() { ir, decorationBindingLiteral, descriptorSetIdLiteral });
    }

    public static void AddDecorationBuiltIn(FrontEndTranslator translator, ShaderOp instanceOp, Spv.BuiltIn builtInType, ShaderBlock decorationsBlock)
    {
      var decorationBuiltInLiteral = translator.CreateConstantLiteral((int)Spv.Decoration.DecorationBuiltIn);
      var builtInTypeLiteral = translator.CreateConstantLiteral((int)builtInType);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpDecorate, null, new List<IShaderIR>() { instanceOp, decorationBuiltInLiteral, builtInTypeLiteral });
    }

    public static void AddDecorationLocation(FrontEndTranslator translator, ShaderOp instanceOp, int location, ShaderBlock decorationsBlock)
    {
      var decorationLocationLiteral = translator.CreateConstantLiteral((int)Spv.Decoration.DecorationLocation);
      var locationLiteral = translator.CreateConstantLiteral(location);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpDecorate, null, new List<IShaderIR>() { instanceOp, decorationLocationLiteral, locationLiteral });
    }

    public delegate bool LocationCallback(ShaderField field, out int location, out int component);

    public static void AddDecorationLocations(FrontEndTranslator translator, ShaderType shaderType, ShaderInterfaceSet interfaceSet, LocationCallback locationCallback,  ShaderBlock decorationsBlock)
    {
      int GetNextLocation(Dictionary<int, ShaderInterfaceField> usedLocations, int currLocation)
      {
        while (usedLocations.ContainsKey(currLocation))
          ++currLocation;
        return currLocation;
      };
      var usedLocations = new Dictionary<int, ShaderInterfaceField>();
      List<(ShaderInterfaceField Field, ShaderOp FieldOp)> unknownLocationFields = new List<(ShaderInterfaceField Field, ShaderOp FieldOp)>();
      foreach (var field in interfaceSet)
      {
        var fieldOp = interfaceSet.GetFieldInstance(translator, field, null);

        int location = -1;
        int component = -1;
        locationCallback(field.ShaderField, out location, out component);
        if (location != -1)
        {
          Decorations.AddDecorationLocation(translator, fieldOp, location, decorationsBlock);
          usedLocations.Add(location, field);
          if(component != -1)
            Decorations.AddDecorationComponent(translator, fieldOp, location, decorationsBlock);
        }
        else
          unknownLocationFields.Add((field, fieldOp));
      }

      var nextLocation = 0;
      foreach (var pair in unknownLocationFields)
      {
        nextLocation = GetNextLocation(usedLocations, nextLocation);
        Decorations.AddDecorationLocation(translator, pair.FieldOp, nextLocation, decorationsBlock);
        usedLocations.Add(nextLocation, pair.Field);
      }
    }

    public static void AddDecorationComponent(FrontEndTranslator translator, ShaderOp instanceOp, int component, ShaderBlock decorationsBlock)
    {
      var decorationLocationLiteral = translator.CreateConstantLiteral((int)Spv.Decoration.DecorationComponent);
      var componentLiteral = translator.CreateConstantLiteral(component);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpDecorate, null, new List<IShaderIR>() { instanceOp, decorationLocationLiteral, componentLiteral });
    }

    public static void AddDecorationMemberOffset(FrontEndTranslator translator, ShaderType shaderType, int fieldIndex, UInt32 byteOffset, ShaderBlock decorationsBlock)
    {
      var decorationOffsetLiteral = translator.CreateConstantLiteral((int)Spv.Decoration.DecorationOffset);
      var fieldIndexLiteral = translator.CreateConstantLiteral(fieldIndex);
      var byteOffsetLiteral = translator.CreateConstantLiteral(byteOffset);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpMemberDecorate, null, new List<IShaderIR>() { shaderType, fieldIndexLiteral, decorationOffsetLiteral, byteOffsetLiteral });
    }

    public static void AddDecorationMemberMatrixStride(FrontEndTranslator translator, ShaderType shaderType, int fieldIndex, int stride, ShaderBlock decorationsBlock)
    {
      var decorationOffsetLiteral = translator.CreateConstantLiteral((int)Spv.Decoration.DecorationMatrixStride);
      var fieldIndexLiteral = translator.CreateConstantLiteral(fieldIndex);
      var strideLiteral = translator.CreateConstantLiteral(stride);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpMemberDecorate, null, new List<IShaderIR>() { shaderType, fieldIndexLiteral, decorationOffsetLiteral, strideLiteral });
    }

    public static void AddDecorationMemberMatrixMajor(FrontEndTranslator translator, ShaderType shaderType, int fieldIndex, Spv.Decoration decoration, ShaderBlock decorationsBlock)
    {
      var decorationOffsetLiteral = translator.CreateConstantLiteral((int)decoration);
      var fieldIndexLiteral = translator.CreateConstantLiteral(fieldIndex);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpMemberDecorate, null, new List<IShaderIR>() { shaderType, fieldIndexLiteral, decorationOffsetLiteral });
    }

    public static void AddDecorationMemberRowMajor(FrontEndTranslator translator, ShaderType shaderType, int fieldIndex, ShaderBlock decorationsBlock)
    {
      AddDecorationMemberMatrixMajor(translator, shaderType, fieldIndex, Spv.Decoration.DecorationRowMajor, decorationsBlock);
    }

    public static void AddDecorationMemberColMajor(FrontEndTranslator translator, ShaderType shaderType, int fieldIndex, ShaderBlock decorationsBlock)
    {
      AddDecorationMemberMatrixMajor(translator, shaderType, fieldIndex, Spv.Decoration.DecorationColMajor, decorationsBlock);
    }

    public static void DecorateUniforms(FrontEndTranslator translator, ShaderOp instanceOp, ShaderBlock decorationsBlock)
    {
      var instanceType = instanceOp.mResultType.GetDereferenceType();
      AddDecorationDescriptorSet(translator, instanceType, 0, decorationsBlock);
      AddDecorationBinding(translator, instanceType, 0, decorationsBlock);
      AddDecorationBlock(translator, instanceType, decorationsBlock);
      DecorateOffsets(translator, instanceType, decorationsBlock);
    }

    public static void DecorateOffsets(FrontEndTranslator translator, ShaderType shaderType, ShaderBlock decorationsBlock)
    {
      UInt32 currentByteOffset = 0;
      for (var fieldIndex = 0; fieldIndex < shaderType.mFields.Count; ++fieldIndex)
      {
        var field = shaderType.mFields[fieldIndex];
        var fieldType = field.mType;
        // Recursively decorate structs
        if (fieldType.mBaseType == OpType.Struct)
          DecorateOffsets(translator, fieldType, decorationsBlock);

        var requiredAlignment = fieldType.ComputeByteAlignment();
        var byteSize = fieldType.ComputeByteSize();
        currentByteOffset = ShaderType.ComputeSizeAfterAlignment(currentByteOffset, requiredAlignment);
        AddDecorationMemberOffset(translator, shaderType, fieldIndex, currentByteOffset, decorationsBlock);

        currentByteOffset += byteSize;
      }
    }

    public static void DecorateStrides(FrontEndTranslator translator, ShaderType shaderType, ShaderBlock decorationsBlock)
    {
      for (var fieldIndex = 0; fieldIndex < shaderType.mFields.Count; ++fieldIndex)
      {
        var field = shaderType.mFields[fieldIndex];
        var fieldType = field.mType;
        // Recursively decorate structs
        if (fieldType.mBaseType == OpType.Struct)
          DecorateStrides(translator, fieldType, decorationsBlock);
        else if(fieldType.mBaseType == OpType.Matrix)
        {
          // Hardcode stride to size of a vec4 for performance reasons.
          // @JoshD: Maybe make a packing option for this later?
          int matrixStride = 16;
          AddDecorationMemberMatrixStride(translator, shaderType, fieldIndex, matrixStride, decorationsBlock);
        }
      }
    }

    public static void DecorateMatrixMajor(FrontEndTranslator translator, ShaderType shaderType, ShaderBlock decorationsBlock)
    {
      for (var fieldIndex = 0; fieldIndex < shaderType.mFields.Count; ++fieldIndex)
      {
        var field = shaderType.mFields[fieldIndex];
        var fieldType = field.mType;
        // Recursively decorate structs
        if (fieldType.mBaseType == OpType.Struct)
          DecorateMatrixMajor(translator, fieldType, decorationsBlock);
        else if (fieldType.mBaseType == OpType.Matrix)
          AddDecorationMemberColMajor(translator, shaderType, fieldIndex, decorationsBlock);
      }
    }
  }

  public class InputOutputLocation
  {
    public int Location = -1;
    public int Component = -1;
  }

}
