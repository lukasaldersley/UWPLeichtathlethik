using System;
using System.Net.Http;
using System.Threading;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http.Filters;
using Windows.Storage;

namespace UWPLeichtathlethik
{
    class Speicherinterface
    {

        private static Windows.Web.Http.Filters.HttpBaseProtocolFilter filter;
        private static HttpClient httpClient;
        private static CancellationTokenSource cts;
        private static bool isFilterUsed;

        public static async Task<String> Download(String src)
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
            //filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            //filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);

            HttpResponseMessage response = await httpClient.GetAsync(resourceUri);
            String output = await response.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine("!" + output + "!");
            isFilterUsed = true;
            return output;
        }
        
        public async void SaveToDocuments(String finleName,String content)
        {
            ;
        }
        
        public async void SaveToLocalFolder(String fileName,String content)
        {
            // Create sample file; replace if exists.
            StorageFolder storageFolder =ApplicationData.Current.LocalFolder;
            StorageFile sampleFile =await storageFolder.CreateFileAsync(fileName,CreationCollisionOption.replaceExisting);
            //sampleFile =await storageFolder.GetFileAsync(fileName); //unter umständen nötig
            await FileIO.WriteTextAsync(sampleFile, content);
        }
        
        public async Task<String> ReadFromLocalFolder(String fileName)
        {
            StorageFolder storageFolder =ApplicationData.Current.LocalFolder;
            StorageFile sampleFile =await storageFolder.GetFileAsync(fileName);
            return await FileIO.ReadTextAsync(sampleFile);
        }
        
        public async Task<String> ReadFromDocuments(String fileName)
        {
            ;
        }
    }
}



