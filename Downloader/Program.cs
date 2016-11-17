using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Downloader.Models;
using Downloader.Utils;

namespace Downloader
{
    internal class Program
    {
        private static readonly Stopwatch Stopwatch = new Stopwatch();

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

            Directory.CreateDirectory(downloadTask.OutputFolder);

            Stopwatch.Start();
            Download(downloadTask);
            Stopwatch.Stop();

            var elapsedTime = Stopwatch.ElapsedMilliseconds/1000;

            Console.WriteLine($"Загрузка завершена успешно. Время загрузки: {elapsedTime}");
        }

        private static void Download(DownloadTask downloadTask)
        {
            Parallel.ForEach(downloadTask.Links, new ParallelOptions {MaxDegreeOfParallelism = downloadTask.LimitRate},
                link => DownloadLink(downloadTask.OutputFolder, link.Url, link.FileName));
        }

        private static void DownloadLink(string folder, string url, string fileName)
        {
            var webClient = new WebClient();
            var downloadPath = $"{folder}\\{fileName}";
            webClient.DownloadFile(url, downloadPath);
        }
    }
}