using System;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace dotnet_azure.Common
{
  public class AppProfile
  {
    internal bool isLinux;

    public string AppPath { get; set; }
    public string PublishName { get; set; }
    
    public DateTime LastPublish { get; set; }
    public string ResourceGroup { get; internal set; }
    public PricingTier PricingTier { get; internal set; }
    public Region Region { get; internal set; }
  }
}