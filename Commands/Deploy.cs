using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using dotnet_azure.Common;
using dotnet_azure.Utilities;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Rest;
using Newtonsoft.Json;
using Kurukuru;
using System.Net.Http;
using System.Text;

namespace dotnet_azure
{
  partial class App
  {
    [Command("deploy", Description = "Deploy application to Azure App Service")]
    private class Deploy
    {
      
      [Argument(0)]
      public string AppPath { get; set; } = "./";

      public string AppProfileFileName
      {
        get
        {
          var info = new DirectoryInfo(AppPath);
          return string.Join("_", info.Name.Split(Path.GetInvalidFileNameChars()));
        }
      }

      private IReadOnlyList<string> RemainingArguments { get; }
      private void OnExecute(IConsole console)
      {
        // do action here
        CreateWebApp().GetAwaiter().GetResult();
      }

      public IAzure GetSdkClient()
      {
        // check to see if token file exits
        if (!File.Exists(Utilities.Settings.TokenFile))
        {
          throw new LogingException("Please Login to Azure. If you do not have an Azure Account, create a FREE account at https://aka.ms/dotnet-azure.");
        }

        var auth = JsonConvert.DeserializeObject<AuthResult>(File.ReadAllText(Utilities.Settings.TokenFile));

        // is token still valid? If not login
        var expires = Convert.ToDateTime(auth.expiresOn);

        if (DateTime.Now >= expires)
        {
          throw new LogingException("Login token has expired, please login to Azure.");
        }

        // login and return IAzure
        var customTokenProvider = new AzureCredentials(
                              new TokenCredentials(auth.token),
                              new TokenCredentials(auth.token),
                              auth.tenantId,
                              AzureEnvironment.AzureGlobalCloud);

        var client = RestClient
                      .Configure()
                      .WithEnvironment(AzureEnvironment.AzureGlobalCloud)
                      .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                      .WithCredentials(customTokenProvider)
                      .Build();

        var authenticatedClient = Azure.Authenticate(client, auth.tenantId).WithDefaultSubscription();

        // Console.WriteLine(authenticatedClient.SubscriptionId);
        return authenticatedClient;
      }

      public async Task CreateWebApp()
      {

        var appProfile = GetAppProfile();
        IWebApp webApp = null;


        await Spinner.StartAsync("", async spinner =>
        {
          spinner.Text = $"Starting deployment for {appProfile.profile.PublishName}";

          if (appProfile.isNew)
          {
            spinner.Text = $"Creating web app {appProfile.profile.PublishName}";

            // create the webapp
            try
            {
              webApp = await GetSdkClient()
                .WebApps.Define(appProfile.profile.PublishName)
                .WithRegion(appProfile.profile.Region)
                .WithNewResourceGroup(appProfile.profile.ResourceGroup)
                .WithNewSharedAppServicePlan()
                .CreateAsync();
            }
            catch (LogingException loginEx)
            {
              spinner.Fail(loginEx.Message);
              return;
            }
            catch (Exception ex)
            {
              spinner.Fail($"Error creating web app {appProfile.profile.PublishName} - " + ex.Message);
              return;
            }
          }
          else
          {
            spinner.Text = $"{appProfile.profile.PublishName} already published, updating files.";

            webApp = GetSdkClient()
            .WebApps.GetByResourceGroup(appProfile.profile.ResourceGroup, appProfile.profile.PublishName);
          }

          // get the zipdeploy url
          var webUri = new Uri(string.Format("https://{0}.scm.azurewebsites.net/api/zipdeploy", appProfile.profile.PublishName));


          spinner.Text = "Retrieving publishing profile.";
          // get the publishing profile and create the auth token
          var publishProfile = webApp.GetPublishingProfile();
          var ftpUser = publishProfile.FtpUsername.Split('\\')[1];
          var val = Convert.ToBase64String(Encoding.Default.GetBytes($"{ftpUser}:{publishProfile.FtpPassword}"));
          string authToken = $"Basic {val}";

          try
          {

            spinner.Text = "Running dotnet publish.";
            // run dotnet publish on application
            try
            {
              var results = ShellHelper.Cmd($"dotnet publish {AppPath} -c Release -o {AppPath}/publish");
              if (results.Contains(": error"))
              {
                throw new DotNetPublishException("Error building application", new Exception(results));
              }
            }
            catch(DotNetPublishException dnex)
            {
              spinner.Fail(dnex.Message);
              return;
            }

            spinner.Text = "Preparing files for upload.";
            //zip publish folder
            var zipFile = ZipContents(appProfile.profile);

            spinner.Text = "Uploading application.";
            var published = await UploadFiles(webUri, zipFile, authToken);
            if (published)
            {
              spinner.Text = $"App {appProfile.profile.PublishName} published.";

              appProfile.profile.LastPublish = DateTime.Now;
              SaveAppProfile(appProfile.profile);
            }
            else
            {
              throw new Exception("Could not deploy files, please try again.");
            }

          }
          catch (Exception ex)
          {
            spinner.Fail($"Error creating web app {appProfile.profile.PublishName} - " + ex.Message);
            throw ex; // throwing so caller can handle
          }
            spinner.Succeed($"Browse to https://{appProfile.profile.PublishName}.azurewebsites.net");
        });
      }

      private async Task<bool> UploadFiles(Uri siteUrl, string zipFile, string authToken)
      {

        using (var fs = File.OpenRead(zipFile))
        using (var client = new HttpClient())
        {

          client.DefaultRequestHeaders.Add("Authorization", authToken);

          var response = await client.PostAsync(siteUrl, new StreamContent(fs));
          return response.IsSuccessStatusCode;
        }
      }

      private string ZipContents(AppProfile profile)
      {
        var zipFile = Settings.ZipFileName(AppProfileFileName);

        if (File.Exists(zipFile))
        {
          File.Delete(zipFile);
        }

        ZipFile.CreateFromDirectory($"{AppPath}/publish", zipFile);
        return zipFile;
      }

      private (AppProfile profile, bool isNew) GetAppProfile()
      {
        var profileData = Settings.AppProfileFileName(AppProfileFileName);

        if (File.Exists(profileData))
        {
          return (profile: JsonConvert.DeserializeObject<AppProfile>(File.ReadAllText(profileData)), isNew: false);
        }

        var publishName = SdkContext.RandomResourceName("webapp", 30);
        var resourceGroup = SdkContext.RandomResourceName("rg", 15);

        var result = new AppProfile();
        result.AppPath = this.AppPath;
        result.PublishName = publishName;
        result.ResourceGroup = resourceGroup;
        result.isLinux = false;
        result.PricingTier = PricingTier.SharedD1;
        result.Region =  Region.USEast.Name;

        return (profile: result, isNew: true);
      }

      private void SaveAppProfile(AppProfile profile)
      {
        var profileData = Settings.AppProfileFileName(AppProfileFileName);
        File.WriteAllText(profileData, JsonConvert.SerializeObject(profile));
      }
    }
  }
}
