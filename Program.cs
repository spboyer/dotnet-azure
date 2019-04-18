using System;
using McMaster.Extensions.CommandLineUtils;

namespace dotnet_azure
{

  //[Subcommand(typeof(GetAzure))]
  [Subcommand(typeof(Deploy))]
  [Subcommand(typeof(Login))]
  partial class App
  {
    public static int Main(string[] args)
    {
      var app = new CommandLineApplication<App>();
      app.Conventions.UseDefaultConventions();

      try
      {
        return app.Execute(args);
      }
      catch (CommandParsingException ex)
      {
        Console.WriteLine($"Unexpected Error {ex.Message}");
        return -1;
      }
    }
    private int OnExecute(CommandLineApplication app, IConsole console)
    {
      console.WriteLine("You must specify at a subcommand.");
      app.ShowHelp();
      return 1;
    }
  }
}