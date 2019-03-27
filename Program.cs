using System;
using System.Collections.Generic;
using McMaster.Extensions.CommandLineUtils;

namespace azure
{

  [Command(Name = "azure-deploy", Description = "A global command set to deploy .NET Core apps to Azure."),
       Subcommand(typeof(Deploy))]
  class Azure
  {
    public static void Main(string[] args) => CommandLineApplication.Execute<Azure>(args);

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
  }
}
