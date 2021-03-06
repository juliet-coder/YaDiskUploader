using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YandexDisk.Client;
using YandexDisk.Client.Http;

namespace ConsoleApp
{
    internal class Program        // C:\Users\Dom\Desktop\Test
    {                             // myFolderTest
        private static string token = "";

        public static async Task Main(string[] args)
        {
            token = ConfigurationManager.AppSettings["oauthToken"];
            var userDir = GetUserLocalFolder(@"Введите адрес локальной директории, например - C:\user\folder\");
            var yaDiskDir = GetYandexDiskDirectory(@"Введите адрес папки на Яндекс Диске (при загрузке в корневой каталог - Enter)");
            IDiskApi diskApi = new DiskHttpApi(token);

            string[] files = Directory.GetFiles(userDir, "*");

            foreach (var file in files)
            {
                
                try
                {
                    file.Remove(0, file.LastIndexOf('\\') + 1);
                    Console.WriteLine(file, "Загрузка");
                    var url = await diskApi.Files.GetUploadLinkAsync("/" + yaDiskDir + "/" + file, overwrite: false);
                    using var fs = File.OpenRead(file);
                    await diskApi.Files.UploadAsync(url, fs);
                    Console.WriteLine(file, "Загружен");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка загрузки файла " + e.Message);
                }
            }
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
    }
}