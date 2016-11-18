using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Downloader.Models
{
    public class DownloadTask
    {
        public DownloadTask(int threadNumber, long limitRate, ConcurrentDictionary<string, IList<string>> links, string outputFolder)
        {
            Threads = threadNumber;
            LimitRate = limitRate;
            Links = links;
            OutputFolder = outputFolder;
        }

        public int Threads { get; private set; }
        public long LimitRate { get; private set; }
        public ConcurrentDictionary<string, IList<string>> Links { get; private set; }
        public string OutputFolder { get; private set; }
    }
}
