using System;

namespace CSShaders
{
  public class CoreShaderLibrary : ShaderLibrary
  {
    public CoreShaderLibrary(FrontEndTranslator frontEnd)
    {
      frontEnd.mCurrentLibrary = this;

      frontEnd.CreatePrimitive(typeof(void), OpType.Void);
      frontEnd.CreatePrimitive(typeof(bool), OpType.Bool);
      // Unsigned int must be first
      CreateIntType(frontEnd, typeof(uint), 32, 0);
      CreateIntType(frontEnd, typeof(int), 32, 1);
      CreateFloatType(frontEnd, typeof(float), 32);
      // @JoshD: Double requires special capabilities, re-add later.
      //CreateFloatType(frontEnd, typeof(double), 64);
    }
    ShaderType CreateIntType(FrontEndTranslator frontEnd, Type type, uint width, uint signedness)
    {
      var floatType = frontEnd.CreatePrimitive(type, OpType.Int);
      floatType.mParameters.Add(frontEnd.CreateConstantLiteral(width));
      floatType.mParameters.Add(frontEnd.CreateConstantLiteral(signedness));
      return floatType;
    }
    ShaderType CreateFloatType(FrontEndTranslator frontEnd, Type type, uint width)
    {
      var floatType = frontEnd.CreatePrimitive(type, OpType.Float);
      floatType.mParameters.Add(frontEnd.CreateConstantLiteral(width));
      return floatType;
    }
  }
}
