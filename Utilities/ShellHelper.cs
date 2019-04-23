using System.Diagnostics;
using System;
using System.Runtime.InteropServices;

namespace dotnet_azure.Utilities
{
  public static class ShellHelper
  {

    public static string Cmd(string cmd)
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        return WinCmd(cmd);
      }

      if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
      {
        return Bash(cmd);
      }

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        return Bash(cmd);
      }

      return "";
    }

    private static string WinCmd(string cmd)
    {
      var escapedArgs = cmd.Replace("\"", "\\\"").Replace("dotnet ",String.Empty);


      var process = new Process()
      {
        StartInfo = new ProcessStartInfo
        {
          FileName = "dotnet",
          Arguments = $"-c \"{escapedArgs}\"",
          RedirectStandardOutput = true,
          UseShellExecute = false,
          CreateNoWindow = true,
        }
      };
      process.Start();
      string result = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      return result;
    }
    private static string Bash(string cmd)
    {
      var escapedArgs = cmd.Replace("\"", "\\\"");

      var process = new Process()
      {
        StartInfo = new ProcessStartInfo
        {
          FileName = "/bin/bash",
          Arguments = $"-c \"{escapedArgs}\"",
          RedirectStandardOutput = true,
          UseShellExecute = false,
          CreateNoWindow = true,
        }
      };
      process.Start();
      string result = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      return result;
    }
  }
}
