using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace dotnet_azure
{

  [Command(Name = "dotnet azure", Description = "A global command set to deploy .NET Core apps to Azure."),
       Subcommand(typeof(Deploy), typeof(GetAzure))]
  class App
  {
    public static void Main(string[] args) => CommandLineApplication.Execute<App>(args);


    private int OnExecute(CommandLineApplication app, IConsole console)
    {
      console.WriteLine("You must specify at a subcommand.");
      app.ShowHelp();
      return 1;
    }

    [Command("deploy", Description = "Deploy application to Azure App Service")]
    private class Deploy
    {

      [Argument(0)]
      public string AppPath { get; set; } = "./";

      private IReadOnlyList<string> RemainingArguments { get; }
      private void OnExecute(IConsole console)
      {
        // do action here
        CreateWebApp().GetAwaiter().GetResult();
      }

      public IAzure GetSdkClient()
      {

        if (!File.Exists(CLI.fileName))
        {
          CLI.CreateServicePrincipalFile();
        }

        var credentials = SdkContext.AzureCredentialsFactory.FromFile(CLI.fileName);

        return Azure
            .Configure()
            .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
            .Authenticate(credentials)
            .WithDefaultSubscription();
      }

      public async Task CreateWebApp(string regionName = "southcentralus")
      {
        var appName = Guid.NewGuid().ToString();
        var nm = string.Format($"aspnetcoreapp{appName}");
        Console.WriteLine($"Creating web app {nm}");

        try
        {
          var webApp = await GetSdkClient()
              .WebApps
                  .Define(nm)
                  .WithRegion(
                      Region.Create(regionName)
                  )
                  .WithNewResourceGroup(nm)
                  .WithNewWindowsPlan(pricingTier: PricingTier.BasicB1)
                  .CreateAsync();

          Console.WriteLine($"Created web app {nm}");
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error creating web app {nm} - " + ex.Message);
          throw ex; // throwing so caller can handle
        }
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
