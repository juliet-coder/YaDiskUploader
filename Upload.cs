using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using YandexDisk.Client;
using YandexDisk.Client.Clients;

namespace ConsoleApp
{
    internal class Upload
    {
        public static async Task UploadFileAsync(string file, IDiskApi diskApi, string targetDir)
        {
            var fileName = file.Split(@"\")[^1];
            var yaDiskPath = targetDir + fileName;


            await diskApi.Files.UploadFileAsync(path: yaDiskPath,
                overwrite: true,
                localFile: file,
                cancellationToken: CancellationToken.None);

            Console.WriteLine(fileName + " uploaded");
        }

        public static async void UploadAsync(string fileName, IDiskApi diskApi, string targetDir)
        {
            Console.WriteLine(fileName + " uploading");
            await Task.Run(() => UploadFileAsync(fileName, diskApi, targetDir));
        }
        
        public static string GetUploadUrl(string yaDiskPath)
        {
            string url = string.Empty;

            try
            {
                using var wc = new WebClient();
                HttpWebRequest wrqst = (HttpWebRequest)WebRequest.Create($"https://cloud-api.yandex.net/v1/disk/resources/upload?path={yaDiskPath}");
                using var myHttpWebResponse = (HttpWebResponse)wrqst.GetResponse();
                if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    using var dataStream = myHttpWebResponse.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string strResponse = reader.ReadToEnd();
                    JObject response = JObject.Parse(strResponse);
                    url = (string)response.SelectToken("href");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return url;
        }
    }
}
