using dotnet_azure.Utilities;

namespace dotnet_azure
{
  public static class DependencyChecker
  {
    public static bool Ruby()
    {
      var result = ShellHelper.Cmd("command -v ruby");
      return result != string.Empty;
    }
    public static bool Homebrew()
    {
      var result = ShellHelper.Cmd("command -v brew");
      return result != string.Empty;
    }

    public static bool AzureCLI()
    {
      var result = ShellHelper.Cmd("command -v az");
      return result != string.Empty;
    }
  }
}