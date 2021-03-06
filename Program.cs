using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using YandexDisk.Client;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace ConsoleApp
{
    class Program        //  C:\Test
    {
        
        private static string token = "";

        public static async Task Main(string[] args)
        {
            try
            {
                token = ConfigurationManager.AppSettings["oauthToken"];
                var userDir = GetUserLocalFolder(@"Введите адрес локальной директории, например - C:\user\folder\");  //  C:\Test
               var yaDiskDir = GetYandexDiskDirectory(@"Введите адрес папки на Яндекс Диске (при загрузке в корневой каталог - Enter)");
                IDiskApi diskApi = new DiskHttpApi(token);

                string[] files = Directory.GetFiles(userDir, "*.*", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    await GetStartAsync(yaDiskDir, diskApi, file);
                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public static async Task GetStartAsync(string yaDiskDir, IDiskApi diskApi, string file)
        {
            
               
            Link url = await diskApi.Files.GetUploadLinkAsync(yaDiskDir + "/" + file, true)
            .ConfigureAwait(false);

            Console.WriteLine(file, "Загрузка");

            using (FileStream fs = File.OpenRead(file))
            {
                
                await diskApi.Files.UploadAsync(url, fs);
            }

            Console.WriteLine(file, "Загружен");
        }

        private static string GetUserLocalFolder(string inputInternal)
                {
                    Console.Write(inputInternal);
                    return Console.ReadLine();
                }

                public static string GetYandexDiskDirectory(string InputExternal)
                {
                    Console.Write(InputExternal);
                    return Console.ReadLine();
                }

        private static void GetUserInput(out UploadParam uploadParam)
        {
            Console.WriteLine(@"Введите адрес локальной директории, например - C:\user\folder\");
            string userDir = Console.ReadLine();

            Console.WriteLine("Введите адрес папки на Яндекс Диске (при загрузке в корневой каталог - Enter)");
            string yaDiskDir = Console.ReadLine();

            uploadParam = new UploadParam(userDir, yaDiskDir);
        }
    }
}






