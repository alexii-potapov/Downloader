using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Downloader.Models;
using Downloader.Utils;

namespace Downloader
{
    internal class Program
    {
        private static readonly Stopwatch Stopwatch = new Stopwatch();
        private static long _traffic;

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

            Console.WriteLine($"Загрузка завершена успешно. \nВремя загрузки: {elapsedTime} секунд. \nЗагружено: {_traffic} байт.");
        }

        private static void Download(DownloadTask downloadTask)
        {
            Parallel.ForEach(downloadTask.Links, new ParallelOptions {MaxDegreeOfParallelism = downloadTask.LimitRate},
                link => DownloadLink(downloadTask.OutputFolder, link.Key, link.Value));
        }

        private static void DownloadLink(string folder,  string url, IList<string> fileNames)
        {
            var webClient = new WebClient();
            var downloadPath = Path.Combine(folder, fileNames.First());
            webClient.DownloadFile(url, downloadPath);

            var fileSize= new FileInfo(downloadPath).Length;
            _traffic = _traffic + fileSize;

            if (fileNames.Count > 1)
            {
                for (var i = 1; i < fileNames.Count; i++)
                {
                    try
                    {
                        var copyPath = Path.Combine(folder, fileNames[i]);
                        File.Copy(downloadPath, copyPath, true);
                    }
                    catch (IOException copyError)
                    {
                        Console.WriteLine(copyError.Message);
                    }
                }
            }
        }
    }
}