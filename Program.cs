using System;
using System.Configuration;
using System.IO;
using YandexDisk.Client;

namespace ConsoleApp       
{
    class Program
    {
        IDiskApi diskApi;
        Upload uploadOptions = new Upload();
        private const string token = "";

        public static void Main(string[] args)
        {
           
            var token = ConfigurationManager.AppSettings["oauthToken"];
            var sourceDir = GetUserLocalFolder(@"Введите адрес локальной директории, например - C:\user\folder\");  //  C:\Test
            var targetDir = GetYandexDiskDirectory(@"Введите адрес папки на Яндекс Диске (при загрузке в корневой каталог - Enter)");

           

           
        }

        public static void UploadToDisk(string userDir, string targetDir)
        {
            try
            {
                
                string[] files = Directory.GetFiles(userDir, "*.*", SearchOption.AllDirectories);


                int i = 0;
                foreach (var file in files)
                {
                    
                    uploadOptions.UploadAsync(files[i], file, diskApi, targetDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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





