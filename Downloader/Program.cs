using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
                link => DownloadLink(downloadTask.OutputFolder, link.Key, link.Value));
        }

        private static void DownloadLink(string folder,  string url, IList<string> fileNames)
        {
            var webClient = new WebClient();
            var downloadPath = Path.Combine(folder, fileNames.First());
            webClient.DownloadFile(url, downloadPath);

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