namespace Downloader.Models
{
    public class Link
    {
        public Link(string url, string filename)
        {
            Url = url;
            FileName = filename;
        }

        public string Url { get; private set; }
        public string FileName { get; private set; }
    }
}
