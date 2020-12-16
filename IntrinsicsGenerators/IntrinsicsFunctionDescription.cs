using System.Collections.Generic;
using System.Text;

namespace IntrinsicsGenerators
{
  /// <summary>
  /// Helper class to describe an intrinsic function. In particular, this is everything not part of the signature,
  /// that is the name of the intrinsic, the name of the function, and any attributes used to declare the intrinsic.
  /// This is mostly used to avoid a little code duplication between op names and attributes.
  /// </summary>
  public class IntrinsicsFunctionDescription
  {
    public virtual string GetIntrinsicName() { return ""; }
    public virtual string GetAttributes() { return ""; }
    public virtual string GetFunctionName() { return ""; }
  }

  /// <summary>
  /// A function description that builds the attributes from a format string.
  /// </summary>
  public class FormatStringFunctionDescription : IntrinsicsFunctionDescription
  {
    public string OpName;
    public string FunctionName;
    public string FormatStr;

    public override string GetIntrinsicName() { return OpName; }
    public override string GetAttributes() { return string.Format(FormatStr, OpName); }
    public override string GetFunctionName() { return FunctionName; }
  }

  /// <summary>
  /// Builds the function description for intrinsics of the SimpleIntrinsic attribute.
  /// </summary>
  public class SimpleIntrinsicsFunctionDescription : FormatStringFunctionDescription
  {
    public SimpleIntrinsicsFunctionDescription(string opName, string functionName)
    {
      OpName = opName;
      FunctionName = functionName;
      FormatStr = "[Shader.SimpleIntrinsicFunction(\"{0}\")]";
    }
  }

  /// <summary>
  /// Builds the function description for intrinsics of the SimpleExtensionIntrinsic attribute for the Glsl.std.450 extension library.
  /// </summary>
  public class GlslExtensionFunctionDescription : FormatStringFunctionDescription
  {
    public GlslExtensionFunctionDescription(string opName, string functionName)
    {
      OpName = opName;
      FunctionName = functionName;
      FormatStr = "[Shader.SimpleExtensionIntrinsic(\"GLSL.std.450\", \"{0}\")]";
    }
  }

  /// <summary>
  /// Builds the function description for intrinsics using the ImageIntrinsicFunction attribute.
  /// </summary>
  public class ImageIntrinsicFunctionDescription : IntrinsicsFunctionDescription
  {
    string OpName;
    int OperandsLocation = -1;
    List<string> OptionalArgs = new List<string>();
    string FunctionName;

    public ImageIntrinsicFunctionDescription(string opName, string functionName)
    {
      OpName = opName;
      FunctionName = functionName;
    }

    public ImageIntrinsicFunctionDescription(string opName, string optionalArg, int operandsLocation, string functionName)
    {
      OpName = opName;
      FunctionName = functionName;
      OperandsLocation = operandsLocation;
      OptionalArgs.Add(optionalArg);
    }

    public ImageIntrinsicFunctionDescription(string opName, List<string> optionalArgs, int operandsLocation, string functionName)
    {
      OpName = opName;
      FunctionName = functionName;
      OperandsLocation = operandsLocation;
      OptionalArgs = optionalArgs;
    }

    public override string GetIntrinsicName()
    {
      return OpName;
    }

    public override string GetAttributes()
    {
      var builder = new StringBuilder();
      // Build the attribute with the op name
      builder.AppendFormat("[Shader.ImageIntrinsicFunction(\"{0}\"", OpName);
      // Append any optional args if they exist. These are or'd together.
      if (OptionalArgs.Count != 0)
      {
        builder.Append(", ");
        for (var i = 0; i < OptionalArgs.Count; ++i)
        {
          builder.Append(OptionalArgs[i]);
          if (i != OptionalArgs.Count - 1)
            builder.Append(" | ");
        }
      }
      // Append the location if it exists.
      if (OperandsLocation != -1)
      {
        builder.Append(", ");
        builder.Append(OperandsLocation);
      }
      builder.Append(")]");
      return builder.ToString();
    }

    public override string GetFunctionName()
    {
      return FunctionName;
    }
  }
}
