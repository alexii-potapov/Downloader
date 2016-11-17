using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

            var result = StartDownload(downloadTask);

            if (result)
            {
                Console.WriteLine("Загрузка завершена успешно");
            }
        }

        private static bool StartDownload(DownloadTask downloadTask)
        {
            IList<Task> tasks = new List<Task>();

            foreach (var link in downloadTask.Links)
            {
                tasks.Add(Task.Factory.StartNew(() => DownloadLink(downloadTask.OutputFolder, link.Url, link.FileName)));
            }
            Task.WaitAll(tasks.ToArray());

            return true;
        }

        private static void DownloadLink(string folder, string url, string fileName)
        {
            var webClient = new WebClient();
            var downloadPath = folder + '\\' + fileName;
            webClient.DownloadFile(url, downloadPath);
        }
    }
}