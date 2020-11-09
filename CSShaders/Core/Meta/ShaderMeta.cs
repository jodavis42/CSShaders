namespace CSShaders
{
  public class ShaderOpMeta : ShaderIRMeta
  {
  }

  public class ShaderTypeMeta : ShaderIRMeta
  {
  }

  public class ShaderFunctionMeta : ShaderIRMeta
  {
    public bool IsStatic = false;
  }

  public class ShaderFieldMeta : ShaderIRMeta
  {
    public bool IsStatic = false;
  }
}
