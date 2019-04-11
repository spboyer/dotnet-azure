namespace dotnet_azure.Common
{
  public class AuthResult
  {
    public string clientId { get; set; }
    public string tenantId { get; set; }
    public string environment { get; set; }
    public string managementEndpointUrl { get; set; }
    public string resourceManagerEndpointUrl { get; set; }
    public string token { get; set; }
    public string expiresOn { get; set;}
  }
}