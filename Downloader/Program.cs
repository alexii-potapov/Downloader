using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Downloader.Models;
using Downloader.Utils;

namespace Downloader
{
    internal class Program
    {
        private static readonly Stopwatch Stopwatch = new Stopwatch();
        private static long _traffic;
        private static long _limitRate;
        private static int _parallelsCount;

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


            _limitRate = downloadTask.LimitRate;
            Directory.CreateDirectory(downloadTask.OutputFolder);

            Stopwatch.Start();
            Download(downloadTask);
            Stopwatch.Stop();

            var elapsedTime = Stopwatch.ElapsedMilliseconds/1000;

            Console.WriteLine(
                $"Загрузка завершена успешно. \nВремя загрузки: {elapsedTime} секунд. \nЗагружено: {_traffic} байт.");
            Console.ReadLine();
        }

        private static void Download(DownloadTask downloadTask)
        {
            _parallelsCount = 0;
            Parallel.ForEach(downloadTask.Links, new ParallelOptions {MaxDegreeOfParallelism = downloadTask.Threads},
                link => DownloadLink(downloadTask.OutputFolder, link.Key, link.Value));
        }

        private static void DownloadLink(string folder, string url, IList<string> fileNames)
        {
            _parallelsCount++;
            var downloadPath = Path.Combine(folder, fileNames.First());
            var wr = WebRequest.Create(url);
            var ws = wr.GetResponse();
            using (var str = ws.GetResponseStream())
            {
                var inBuf = new byte[100000];
                var bytesReadTotal = 0;

                using (var fstr = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
                {
                    var limit = 102400;

                    while (true)
                    {
                        if (_limitRate != 0 || _limitRate>inBuf.Length*100)
                        {
                            Thread.Sleep(10);
                            limit = (int)(_limitRate / (100 * _parallelsCount));
                        }
                        var n = str.Read(inBuf, 0, limit);
                        if ((n == 0) || (n == -1))
                        {
                            break;
                        }
                        fstr.Write(inBuf, 0, n);
                        bytesReadTotal += n;
                        Console.WriteLine(n);
                    }
                }
                _traffic = _traffic + bytesReadTotal;
                _parallelsCount--;
            }


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