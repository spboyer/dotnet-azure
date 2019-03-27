using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Kurukuru;

namespace dotnet_azure
{
  public class DownloadAzure
  {
    public static int Install()
    {
      var installed = false;

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        var tempFile = Path.ChangeExtension(Path.GetTempFileName(), "msi");
        var msiName = "https://azurecliprod.blob.core.windows.net/msi/azure-cli-latest.msi";

        Spinner.Start($"Download Azure CLI from {msiName}", async () =>
                {
                  Task t = WebUtils.DownloadAsync(msiName, tempFile);
                  t.Wait();
                  await t;
                });

        Spinner.Start($"Running Azure CLI installer at {tempFile}", spinner =>
        {
          var p = Process.Start("msiexec.exe", $"/package \"{tempFile}\"");
          p.WaitForExit();
          installed = true;
        });
      }

      if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
      {
        Spinner.Start("Running Azure CLI installer via homebrew", spinner =>
        {
        // this will almost never fail, ruby is on macOS by default.
        spinner.Info("Checking for dependency of ruby");
          if (!DependencyChecker.Ruby())
          {
            spinner.Fail("ruby required to install azure cli");
            return;
          }
          spinner.Succeed();
        });

        Spinner.Start("Checking for dependency of homebrew", spinner =>
        {
        // we can install homebrew auto
        // ShellHelper("/usr/bin/ruby -e \"$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)\"");
        if (!DependencyChecker.Homebrew())
          {
            spinner.Fail("homebrew required to install azure cli");
            return;
          }
          spinner.Succeed();
        });

        Spinner.Start("Installing azure cli using homebrew", spinner =>
        {
          ShellHelper.Bash("brew update && brew install azure-cli");
          installed = true;
        });
      }

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {

      }

      if (installed)
      {
        Console.WriteLine("Close and reopen this command prompt and run \"az login\" to setup the Azure Command Line");
      }

      return 0;
    }
  }
}