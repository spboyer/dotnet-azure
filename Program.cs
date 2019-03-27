using System;
using System.Collections.Generic;
using McMaster.Extensions.CommandLineUtils;

namespace dotnet_azure
{

  [Command(Name = "dotnet azure", Description = "A global command set to deploy .NET Core apps to Azure."),
       Subcommand(typeof(Deploy), typeof(GetAzure))]
  class Azure
  {
    public static void Main(string[] args) => CommandLineApplication.Execute<Azure>(args);

    [Argument(0)]
    public string AppPath { get; set; } = "./";

    private int OnExecute(CommandLineApplication app, IConsole console)
    {
      console.WriteLine("You must specify at a subcommand.");
      app.ShowHelp();
      return 1;
    }

    [Command("deploy", Description = "Deploy application to Azure App Service")]
    private class Deploy
    {
      private IReadOnlyList<string> RemainingArguments { get; }
      private void OnExecute(IConsole console)
      {
        // do action here
      }

    }

    [Command("get-cli", Description = "Download and install the Azure CLI")]
    private class GetAzure
    {
      private IReadOnlyList<string> RemainingArguments { get; }
      private void OnExecute(IConsole console)
      {
        // do action here
      }
    }
  }
}
