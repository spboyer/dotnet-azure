using System;
using System.IO;
using Microsoft.Azure.Management.AppService.Fluent;

namespace dotnet_azure.Utilities
{
  public static class Settings
  {

    public static PricingTier GetPricingTier(string value)
    {
      if (string.Equals(value, "BasicB1", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.BasicB1;
      if (string.Equals(value, "SharedD1", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.SharedD1;
      if (string.Equals(value, "FreeF1", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.FreeF1;
      if (string.Equals(value, "PremiumP3v2", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.PremiumP3v2;
      if (string.Equals(value, "PremiumP2v2", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.PremiumP2v2;
      if (string.Equals(value, "PremiumP3", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.PremiumP3;
      if (string.Equals(value, "PremiumP2", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.PremiumP2;
      if (string.Equals(value, "PremiumP1v2", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.PremiumP1v2;
      if (string.Equals(value, "StandardS3", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.StandardS3;
      if (string.Equals(value, "StandardS2", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.StandardS2;
      if (string.Equals(value, "StandardS1", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.StandardS1;
      if (string.Equals(value, "BasicB3", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.BasicB3;
      if (string.Equals(value, "BasicB2", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.BasicB2;
      if (string.Equals(value, "PremiumP1", StringComparison.CurrentCultureIgnoreCase)) return PricingTier.PremiumP1;

      return PricingTier.SharedD1;
    }
    public static string TokenFile
    {
      get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "dotnet-azure", "token.json"); }
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