using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Downloader.Utils;

namespace Downloader.Models
{
    public class DownloadTask
    {
        public DownloadTask(int threadNumber, int limitRate, string[] links, string outputFolder)
        {
            Threads = threadNumber;
            LimitRate = limitRate;
            Links = links;
            OutputFolder = outputFolder;
        }

        public int Threads { get; private set; }
        public int LimitRate { get; private set; }
        public string[] Links { get; private set; }
        public string OutputFolder { get; private set; }
    }
}
