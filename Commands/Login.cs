using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace dotnet_azure
{
  partial class App
  {
    [Command("login", Description = "Login into Azure")]
    private class Login
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

        var taskResult = DoLogin(nodeService);

        Task.WaitAll(taskResult);

        if (taskResult.IsCompletedSuccessfully)
        {
          File.WriteAllText(Utilities.Settings.TokenFile, taskResult.Result);

        }
      }
    }
    private static async Task<string> DoLogin(INodeServices nodeService)
    {
      //Invoke the javascript module with parameters to execute in Node environment.
      return await nodeService.InvokeAsync<string>(@"scripts/auth.js");
    }
  }
}