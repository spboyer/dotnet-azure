using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using Newtonsoft.Json;
using dotnet_azure.Common;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Rest;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Fluent;
using System.IO;

namespace dotnet_azure
{
  partial class App
  {

    [Command("login", Description = "Deploy application to Azure App Service")]
    private class AzureLogin
    {
      private void OnExecute(IConsole console)
      {
        //Instantiate DI container for the application
        var serviceCollection = new ServiceCollection();

        //Register NodeServices
        serviceCollection.AddNodeServices(options => options.InvocationTimeoutMilliseconds = 240 * 1000);

        //Request the DI container to supply the shared INodeServices instance
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var nodeService = serviceProvider.GetRequiredService<INodeServices>();

        var taskResult = Login(nodeService);

        Task.WaitAll(taskResult);

        if (taskResult.IsCompletedSuccessfully)
        {
          //Console.WriteLine(taskResult.Result);
          //Console.WriteLine();

          // todo: save the auth file to local
          File.WriteAllText(Utilities.Settings.TokenFile, taskResult.Result);

          //var auth = JsonConvert.DeserializeObject<AuthResult>(taskResult.Result);


          // var customTokenProvider = new AzureCredentials(
          //                     new TokenCredentials(auth.token),
          //                     new TokenCredentials(auth.token),
          //                     auth.tenantId,
          //                     AzureEnvironment.AzureGlobalCloud);

          // var client = RestClient
          //               .Configure()
          //               .WithEnvironment(AzureEnvironment.AzureGlobalCloud)
          //               .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
          //               .WithCredentials(customTokenProvider)
          //               .Build();

          // var authenticatedClient = Azure.Authenticate(client, auth.tenantId).WithDefaultSubscription();

          // Console.WriteLine(authenticatedClient.SubscriptionId);
        }
      }
    }
    private static async Task<string> Login(INodeServices nodeService)
    {
      //Invoke the javascript module with parameters to execute in Node environment.
      return await nodeService.InvokeAsync<string>(@"scripts/auth.js");
    }
  }
}