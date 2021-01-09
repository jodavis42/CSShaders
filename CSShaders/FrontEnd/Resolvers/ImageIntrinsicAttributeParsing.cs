﻿using Microsoft.CodeAnalysis;
using System;

namespace CSShaders
{
  public class ImageIntrinsicAttributeData
  {
    public OpInstructionType OpType = OpInstructionType.OpUndef;
    public Shader.ImageOperands Operands = Shader.ImageOperands.None;
    public int OperandsLocation = -1;


    public static ImageIntrinsicAttributeData Parse(AttributeData attribute, int defaultOperandsLocation = -1)
    {
      if (attribute.ConstructorArguments.Length == 0)
        return null;

      ImageIntrinsicAttributeData result = new ImageIntrinsicAttributeData();
      result.OperandsLocation = defaultOperandsLocation;
      // Load the attribute params if they exist. Note: This isn't really safe, but works for now.
      foreach (var attributeParam in attribute.ConstructorArguments)
      {
        if (attributeParam.Type.Name == typeof(string).Name)
          result.OpType = (OpInstructionType)Enum.Parse(typeof(OpInstructionType), attributeParam.Value as string, true);
        else if (attributeParam.Type.Name == typeof(Shader.ImageOperands).Name)
          result.Operands = (Shader.ImageOperands)attributeParam.Value;
        else if (attributeParam.Type.Name == typeof(UInt32).Name)
          result.OperandsLocation = (int)(UInt32)attributeParam.Value;
      }
      // Also try to resolve any named parameter arguments
      foreach (var pair in attribute.NamedArguments)
      {
        if (pair.Key == "OpName")
          result.OpType = (OpInstructionType)Enum.Parse(typeof(OpInstructionType), pair.Value.Value as string, true);
        else if (pair.Key == "Operands")
          result.Operands = (Shader.ImageOperands)pair.Value.Value;
        else if (pair.Key == "OperandsLocation")
          result.OperandsLocation = (int)pair.Value.Value;
      }
      return result;
    }
  }
}