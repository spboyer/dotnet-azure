using McMaster.Extensions.CommandLineUtils;

namespace dotnet_azure
{

  [Command(Name = "dotnet azure", Description = "A global command set to deploy .NET Core apps to Azure."),
       Subcommand(typeof(Deploy), typeof(GetAzure), typeof(Login))]
  partial class App
  {
    public static void Main(string[] args) => CommandLineApplication.Execute<App>(args);


    private int OnExecute(CommandLineApplication app, IConsole console)
    {
      console.WriteLine("You must specify at a subcommand.");
      app.ShowHelp();
      return 1;
    }
  }
}
