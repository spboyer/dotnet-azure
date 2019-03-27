using System;
using System.IO;

namespace dotnet_azure
{
  public static class CLI
  {
    public static string fileName
    {
      get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "rbac.json"); }
    }
    public static void CreateServicePrincipalFile()
    {
      var result = ShellHelper.Bash("az ad sp create-for-rbac --sdk-auth");

      File.WriteAllText(fileName, result);
    }

    public static bool Login()
    {
      var result = ShellHelper.Bash("az login");
      return result.Contains("You have logged in.");
    }

    public static bool IsLoggedIn()
    {
      var result = ShellHelper.Bash("az account list -o table");
      return result.Contains("az login");
    }
  }
}