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

    public static void AddDecorationMemberOffset(FrontEndTranslator translator, ShaderType shaderType, int fieldIndex, UInt32 byteOffset, ShaderBlock decorationsBlock)
    {
      var decorationOffsetLiteral = translator.CreateConstantLiteral((int)Spv.Decoration.DecorationOffset);
      var fieldIndexLiteral = translator.CreateConstantLiteral(fieldIndex);
      var byteOffsetLiteral = translator.CreateConstantLiteral(byteOffset);
      translator.CreateOp(decorationsBlock, OpInstructionType.OpMemberDecorate, null, new List<IShaderIR>() { shaderType, fieldIndexLiteral, decorationOffsetLiteral, byteOffsetLiteral });
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
  }
}
