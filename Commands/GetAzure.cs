using System;
using System.Collections.Generic;
using dotnet_azure.Actions;
using McMaster.Extensions.CommandLineUtils;

namespace dotnet_azure
{
  partial class App
  {
    [Command("get-cli", Description = "Download and install the Azure CLI")]
    private class GetAzure
    {
      private IReadOnlyList<string> RemainingArguments { get; }
      private void OnExecute(IConsole console)
      {
        if (!DependencyChecker.AzureCLI())
        {
          DownloadAzure.Install();
        }
        else
        {
          Console.WriteLine("Azure CLI currently installed.");
        }
      }
    }
  }
}
