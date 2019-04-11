using System;
using System.IO;

namespace dotnet_azure.Utilities
{
  public class Settings
  {
    public static string TokenFile
    {
      get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "token.json"); }
    }
  }
}