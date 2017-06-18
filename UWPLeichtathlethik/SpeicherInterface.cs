using System;
using System.Net.Http;
using System.Threading;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http.Filters;

namespace UWPLeichtathlethik
{
    class Speicherinterface
    {

        private static Windows.Web.Http.Filters.HttpBaseProtocolFilter filter;
        private static HttpClient httpClient;
        private static CancellationTokenSource cts;
        private static bool isFilterUsed;

        public static async void Download(String src)
        {
            filter = new Windows.Web.Http.Filters.HttpBaseProtocolFilter();
            httpClient = new HttpClient();
            cts = new CancellationTokenSource();
            isFilterUsed = false;
            Uri resourceUri;


            if (!Uri.TryCreate(src.Trim(), UriKind.Absolute, out resourceUri))
            {
                return;
            }

            filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.Default;
            filter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.Default;

            // ---------------------------------------------------------------------------
            // WARNING: Only test applications should ignore SSL errors.
            // In real applications, ignoring server certificate errors can lead to MITM
            // attacks (while the connection is secure, the server is not authenticated).
            //
            // The SetupServer script included with this sample creates a server certificate that is self-signed
            // and issued to fabrikam.com, and hence we need to ignore these errors here. 
            // ---------------------------------------------------------------------------
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);

            HttpResponseMessage response = await httpClient.GetAsync(resourceUri);
            String output = await response.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine("!" + output + "!");
            isFilterUsed = true;
        }
    }
}



