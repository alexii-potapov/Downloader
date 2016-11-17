using System;
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
            }
        }
        
        
    }
}