using System;
using System.Linq;
using System.Net;
using Downloader.Models;
using Downloader.Utils;

namespace Downloader
{
    internal class Program
    {
        private static void Main(string[] arguments)
        {

            DownloadTask downloadTask;
            try
            {
                downloadTask = AgumentParser.ParseParams(arguments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            //Todo 
            //CreateFolder downloadTask.OutputFolder

            var result =StartDownload(downloadTask);

            if (result)
            {
                Console.WriteLine("Загрузка завершена успешно");
            }


        }

        private static bool StartDownload(DownloadTask downloadTask)
        {
            WebClient myWebClient = new WebClient();

            foreach (var link in downloadTask.Links)
            {
                var downloadPath = downloadTask.OutputFolder + '\\' + link.Split('/').Last();

                myWebClient.DownloadFile(link, downloadPath);
            }

            return true;

        }
    }
}