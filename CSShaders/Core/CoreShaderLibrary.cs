using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSShaders
{
  public class CoreShaderLibrary : ShaderLibrary
  {
    public CoreShaderLibrary(FrontEndTranslator translator)
    {
      translator.mCurrentLibrary = this;

      // Create core primitive times first
      translator.CreatePrimitive(typeof(void), OpType.Void);
      translator.CreatePrimitive(typeof(bool), OpType.Bool);
      // Unsigned int must be first
      CreateIntType(translator, typeof(uint), 32, 0);
      CreateIntType(translator, typeof(int), 32, 1);
      var floatType = CreateFloatType(translator, typeof(float), 32);
      // @JoshD: Double requires special capabilities, re-add later.
      //CreateFloatType(frontEnd, typeof(double), 64);

      // Add some custom intrinsics for built-int types that 
      AddPrimitiveIntrinsics(translator);

      // Compile the custom data types and functions we're adding
      // @JoshD: Fix path lookup
      var path = Path.Combine("..", "Shader");
      var extensions = new HashSet<string> { ".cs" };
      var project = new ShaderProject();
      project.LoadProjectDirectory(path, extensions);
      var trees = new List<SyntaxTree>();
      SourceCompilation = project.Compile("Core", null, translator, out trees);
      project.Translate(SourceCompilation, trees, null, translator, this);
    }

    ShaderType CreateIntType(FrontEndTranslator translator, Type type, uint width, uint signedness)
    {
      var floatType = translator.CreatePrimitive(type, OpType.Int);
      floatType.mParameters.Add(translator.CreateConstantLiteral(width));
      floatType.mParameters.Add(translator.CreateConstantLiteral(signedness));
      return floatType;
    }

    ShaderType CreateFloatType(FrontEndTranslator translator, Type type, uint width)
    {
      var floatType = translator.CreatePrimitive(type, OpType.Float);
      floatType.mParameters.Add(translator.CreateConstantLiteral(width));
      return floatType;
    }

    void AddPrimitiveIntrinsics(FrontEndTranslator translator)
    {
      var floatType = translator.FindType(typeof(float));
      SpecialResolvers.CreateSimpleIntrinsicFunction(translator, new FunctionKey("float.operator +(float, float)"), OpInstructionType.OpFAdd, floatType);
    }
  }
}
