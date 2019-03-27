using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnet_azure
{
  public static class WebUtils
  {
    //private static Lazy<IWebProxy> proxy = new Lazy<IWebProxy>(() => string.IsNullOrEmpty(Settings.Default.WebProxyAddress) ? null : new WebProxy { Address = new Uri(Settings.Default.WebProxyAddress), UseDefaultCredentials = true });

    public static IWebProxy Proxy
    {
      get { return null; } //WebUtils.proxy.Value; }
    }

    public static Task DownloadAsync(string requestUri, string filename)
    {
      if (requestUri == null)
        throw new ArgumentNullException("requestUri");

      return DownloadAsync(new Uri(requestUri), filename);
    }

    public static async Task DownloadAsync(Uri requestUri, string filename)
    {
      if (filename == null)
        throw new ArgumentNullException("filename");

      if (Proxy != null)
      {
        WebRequest.DefaultWebProxy = Proxy;
      }

      using (var httpClient = new HttpClient())
      {
        using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
        {
          using (
              Stream contentStream = await (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync(),
              stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
          {
            await contentStream.CopyToAsync(stream);
          }
        }
      }
    }
  }
}