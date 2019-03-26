public static class DependencyChecker
{
  public static bool Ruby()
  {
    var result = ShellHelper.Bash("command -v ruby");
    return result != string.Empty;
  }
  public static bool Homebrew()
  {
    var result = ShellHelper.Bash("command -v brew");
    return result != string.Empty;
  }

  public static bool AzureCLI()
  {
    var result = ShellHelper.Bash("command -v az");
    return result != string.Empty;
  }
}