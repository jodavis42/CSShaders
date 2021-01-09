using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CSShadersTests
{
  public class CommandLineTool
  {
    static protected string GenerateFilePath(string filePath, string newExtension, bool emitToTempDir = false)
    {
      if (emitToTempDir)
      {
        string newPath = Path.ChangeExtension(filePath, newExtension);
        return Path.Combine(Path.GetTempPath(), Path.GetFileName(newPath));
      }
      else
        return Path.ChangeExtension(filePath, newExtension);
    }

    static protected void RunProcessSimple(string fileName, string arguments, bool shellExecute)
    {
      string stdOut = "", stdErr = "";
      RunProcessSimple(fileName, arguments, shellExecute, ref stdOut, ref stdErr);
    }

    static protected void RunProcessSimple(string fileName, string arguments, bool shellExecute, ref string stdOut, ref string stdErr)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = fileName;
      startInfo.Arguments = arguments;
      startInfo.UseShellExecute = shellExecute;
      if (!shellExecute)
      {
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
      }

      var process = Process.Start(startInfo);
      if (startInfo.RedirectStandardOutput == false)
        return;

      if(stdOut != null)
        stdOut = process.StandardOutput.ReadToEnd();
      if(stdErr != null)
        stdErr = process.StandardError.ReadToEnd();
    }
  }

  public class DiffTool : CommandLineTool
  {
    public string mDiffText = "";
    public string mErrorText = "";
    static string mDiffPath = Path.Combine("Tools", "DiffUtil", "diff.exe");

    public virtual bool Diff(string expected, string actual)
    {
      var args = String.Format("\"{0}\" \"{1}\"", expected, actual);
      RunProcessSimple(mDiffPath, args, false, ref mDiffText, ref mErrorText);
      return mDiffText.Length == 0 & mErrorText.Length == 0;
    }
  }

  public class TortoiseDiffTool : DiffTool
  {
    public bool mVisualDisplay = false;
    static string mTortoiseDiffPath = "tortoisemerge.exe";
    public override bool Diff(string expected, string actual)
    {
      if (!File.Exists(expected))
      {
        var file = new FileStream(expected, FileMode.Create);
        file.Close();
      }

      if (base.Diff(expected, actual))
        return true;

      if(mVisualDisplay)
      {
        var args = String.Format("\"{0}\" \"{1}\"", actual, expected);
        RunProcessSimple(mTortoiseDiffPath, args, false);
      }
      return false;
    }
  }

  public class SpirVTool : CommandLineTool
  {
  }

  public class SpirVDisassemblerTool : CommandLineTool
  {
    public string Run(string inputFile)
    {
      inputFile = Path.GetFullPath(inputFile);
      string arguments = String.Format("\"{0}\"", inputFile);
      string stdOut = "", stdErr = "";
      RunProcessSimple("spirv-dis.exe", arguments, false, ref stdOut, ref stdErr);
      return stdErr + stdOut;
    }
    public void Run(string inputFile, string outFile)
    {
      inputFile = Path.GetFullPath(inputFile);
      string arguments = String.Format("{2} -o \"{0}\" \"{1}\"", outFile, inputFile, "");
      RunProcessSimple("spirv-dis.exe", arguments, false);
    }
  }

  public class SpirVValidatorTool : CommandLineTool
  {
    public string Run(string inputFile)
    {
      inputFile = Path.GetFullPath(inputFile);
      string arguments = String.Format("\"{0}\" --target-env {1}", inputFile, "spv1.4");
      string stdOut = "", stdErr = "";
      RunProcessSimple("spirv-val.exe", arguments, false, ref stdOut, ref stdErr);
      return stdErr + stdOut;
    }

    public bool Run(string inputFile, string outFile)
    {
      string result = Run(inputFile);
      File.WriteAllText(outFile, result);
      return result.Length == 0;
    }
  }

  public class SpirVCrossTool : CommandLineTool
  {
    public string Run(string inputFile)
    {
      inputFile = Path.GetFullPath(inputFile);
      string arguments = String.Format("\"{0}\"", inputFile);
      string stdOut = "", stdErr = "";
      RunProcessSimple("SPIRV-Cross.exe", arguments, false, ref stdOut, ref stdErr);
      return stdErr + stdOut;
    }

    public void Run(string inputFile, string outFile)
    {
      inputFile = Path.GetFullPath(inputFile);
      string arguments = String.Format("\"{0}\" --output \"{1}\"", inputFile, outFile);
      RunProcessSimple("SPIRV-Cross.exe", arguments, false);
    }
  }
}
