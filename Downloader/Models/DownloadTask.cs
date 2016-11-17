using System.Collections.Generic;

namespace Downloader.Models
{
    public class DownloadTask
    {
        public DownloadTask(int threadNumber, int limitRate, IList<Link> links, string outputFolder)
        {
            Threads = threadNumber;
            LimitRate = limitRate;
            Links = links;
            OutputFolder = outputFolder;
        }

        public int Threads { get; private set; }
        public int LimitRate { get; private set; }
        public IList<Link> Links { get; private set; }
        public string OutputFolder { get; private set; }
    }
}
