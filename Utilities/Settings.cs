using System;
using System.IO;

namespace dotnet_azure.Utilities
{
  public static class Settings
  {
    public static string TokenFile
    {
      get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"dotnet-azure", "token.json"); }
    }

    internal static string AppProfileFileName(string cleanAppProfileName)
    {
      return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "dotnet-azure", $"{cleanAppProfileName}.json");
    }

    internal static string ZipFileName(string cleanAppProfileName)
    {
      return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "dotnet-azure", $"{cleanAppProfileName}.zip");
    }
  }
}