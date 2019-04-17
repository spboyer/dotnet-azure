using System;
using System.IO;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace dotnet_azure.Utilities
{
  public static class Settings
  {
    public static Region GetRegionByName(string value)
    {
      if (string.Equals(value, "USWest", StringComparison.CurrentCultureIgnoreCase)) return Region.USWest;
      if (string.Equals(value, "AustraliaCentral2", StringComparison.CurrentCultureIgnoreCase)) return Region.AustraliaCentral2;
      if (string.Equals(value, "IndiaSouth", StringComparison.CurrentCultureIgnoreCase)) return Region.IndiaSouth;
      if (string.Equals(value, "IndiaWest", StringComparison.CurrentCultureIgnoreCase)) return Region.IndiaWest;
      if (string.Equals(value, "KoreaSouth", StringComparison.CurrentCultureIgnoreCase)) return Region.KoreaSouth;
      if (string.Equals(value, "KoreaCentral", StringComparison.CurrentCultureIgnoreCase)) return Region.KoreaSouth;
      if (string.Equals(value, "AustraliaCentral", StringComparison.CurrentCultureIgnoreCase)) return Region.AustraliaCentral;
      if (string.Equals(value, "GermanyCentral", StringComparison.CurrentCultureIgnoreCase)) return Region.GermanyCentral;
      if (string.Equals(value, "GermanyNorthEast", StringComparison.CurrentCultureIgnoreCase)) return Region.GermanyNorthEast;
      if (string.Equals(value, "AustraliaSouthEast", StringComparison.CurrentCultureIgnoreCase)) return Region.AustraliaSouthEast;
      if (string.Equals(value, "IndiaCentral", StringComparison.CurrentCultureIgnoreCase)) return Region.IndiaCentral;
      if (string.Equals(value, "JapanWest", StringComparison.CurrentCultureIgnoreCase)) return Region.JapanWest;
      if (string.Equals(value, "USWest2", StringComparison.CurrentCultureIgnoreCase)) return Region.USWest2;
      if (string.Equals(value, "USCentral", StringComparison.CurrentCultureIgnoreCase)) return Region.USCentral;
      if (string.Equals(value, "USEast", StringComparison.CurrentCultureIgnoreCase)) return Region.USEast;
      if (string.Equals(value, "USEast2", StringComparison.CurrentCultureIgnoreCase)) return Region.USEast2;
      if (string.Equals(value, "USNorthCentral", StringComparison.CurrentCultureIgnoreCase)) return Region.USNorthCentral;
      if (string.Equals(value, "AustraliaEast", StringComparison.CurrentCultureIgnoreCase)) return Region.AustraliaEast;
      if (string.Equals(value, "USWestCentral", StringComparison.CurrentCultureIgnoreCase)) return Region.USWestCentral;
      if (string.Equals(value, "CanadaCentral", StringComparison.CurrentCultureIgnoreCase)) return Region.CanadaCentral;
      if (string.Equals(value, "CanadaEast", StringComparison.CurrentCultureIgnoreCase)) return Region.CanadaEast;
      if (string.Equals(value, "USSouthCentral", StringComparison.CurrentCultureIgnoreCase)) return Region.USSouthCentral;
      if (string.Equals(value, "EuropeNorth", StringComparison.CurrentCultureIgnoreCase)) return Region.EuropeNorth;
      if (string.Equals(value, "EuropeWest", StringComparison.CurrentCultureIgnoreCase)) return Region.EuropeWest;
      if (string.Equals(value, "UKSouth", StringComparison.CurrentCultureIgnoreCase)) return Region.UKSouth;
      if (string.Equals(value, "UKWest", StringComparison.CurrentCultureIgnoreCase)) return Region.UKWest;
      if (string.Equals(value, "AsiaEast", StringComparison.CurrentCultureIgnoreCase)) return Region.AsiaEast;
      if (string.Equals(value, "AsiaSouthEast", StringComparison.CurrentCultureIgnoreCase)) return Region.AsiaSouthEast;
      if (string.Equals(value, "JapanEast", StringComparison.CurrentCultureIgnoreCase)) return Region.JapanEast;
      if (string.Equals(value, "BrazilSouth", StringComparison.CurrentCultureIgnoreCase)) return Region.BrazilSouth;

      // CLI Values
      if (string.Equals(value, "eastasia", StringComparison.CurrentCultureIgnoreCase)) return Region.AsiaEast;
      if (string.Equals(value, "southeastasia", StringComparison.CurrentCultureIgnoreCase)) return Region.AsiaSouthEast;
      if (string.Equals(value, "centralus", StringComparison.CurrentCultureIgnoreCase)) return Region.USCentral;
      if (string.Equals(value, "eastus", StringComparison.CurrentCultureIgnoreCase)) return Region.USEast;
      if (string.Equals(value, "eastus2", StringComparison.CurrentCultureIgnoreCase)) return Region.USEast2;
      if (string.Equals(value, "westus", StringComparison.CurrentCultureIgnoreCase)) return Region.USWest;
      if (string.Equals(value, "northcentralus", StringComparison.CurrentCultureIgnoreCase)) return Region.USNorthCentral;
      if (string.Equals(value, "southcentralus", StringComparison.CurrentCultureIgnoreCase)) return Region.USSouthCentral;
      if (string.Equals(value, "northeurope", StringComparison.CurrentCultureIgnoreCase)) return Region.EuropeNorth;
      if (string.Equals(value, "westeurope", StringComparison.CurrentCultureIgnoreCase)) return Region.EuropeWest;
      if (string.Equals(value, "japanwest", StringComparison.CurrentCultureIgnoreCase)) return Region.JapanWest;
      if (string.Equals(value, "japaneast", StringComparison.CurrentCultureIgnoreCase)) return Region.JapanEast;
      if (string.Equals(value, "brazilsouth", StringComparison.CurrentCultureIgnoreCase)) return Region.BrazilSouth;
      if (string.Equals(value, "australiaeast", StringComparison.CurrentCultureIgnoreCase)) return Region.AustraliaEast;
      if (string.Equals(value, "australiasoutheast", StringComparison.CurrentCultureIgnoreCase)) return Region.AustraliaSouthEast;
      if (string.Equals(value, "southindia", StringComparison.CurrentCultureIgnoreCase)) return Region.IndiaSouth;
      if (string.Equals(value, "centralindia", StringComparison.CurrentCultureIgnoreCase)) return Region.IndiaCentral;
      if (string.Equals(value, "westindia", StringComparison.CurrentCultureIgnoreCase)) return Region.IndiaWest;
      if (string.Equals(value, "canadacentral", StringComparison.CurrentCultureIgnoreCase)) return Region.CanadaCentral;
      if (string.Equals(value, "canadaeast", StringComparison.CurrentCultureIgnoreCase)) return Region.CanadaEast;
      if (string.Equals(value, "uksouth", StringComparison.CurrentCultureIgnoreCase)) return Region.UKSouth;
      if (string.Equals(value, "ukwest", StringComparison.CurrentCultureIgnoreCase)) return Region.UKWest;
      if (string.Equals(value, "westcentralus", StringComparison.CurrentCultureIgnoreCase)) return Region.USWestCentral;
      if (string.Equals(value, "westus2", StringComparison.CurrentCultureIgnoreCase)) return Region.USWest2;
      if (string.Equals(value, "koreacentral", StringComparison.CurrentCultureIgnoreCase)) return Region.KoreaCentral;
      if (string.Equals(value, "koreasouth", StringComparison.CurrentCultureIgnoreCase)) return Region.KoreaSouth;
      if (string.Equals(value, "australiacentral", StringComparison.CurrentCultureIgnoreCase)) return Region.AustraliaCentral;
      if (string.Equals(value, "australiacentral2", StringComparison.CurrentCultureIgnoreCase)) return Region.AustraliaCentral2;

      // Not Available in SDK yet
      //if (string.Equals(value, "francecentral", StringComparison.CurrentCultureIgnoreCase)) return Region.FranceNorth;
      //if (string.Equals(value, "francesouth", StringComparison.CurrentCultureIgnoreCase)) return Region.FranceSouth;
      //if (string.Equals(value, "southafricanorth", StringComparison.CurrentCultureIgnoreCase)) return Region.SouthAfricaNorth;
      //if (string.Equals(value, "southafricawest", StringComparison.CurrentCultureIgnoreCase)) return Region.SouthAfricaWest;

      return Region.USEast;
    }

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