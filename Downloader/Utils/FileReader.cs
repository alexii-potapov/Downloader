using System;
using System.IO;

namespace Downloader.Utils
{
    public static class FileReader
    {
        public static string[] GetLinks(string path)
        {
            try
            {
                using (var sr = new StreamReader(path))
                {
                    var line = sr.ReadToEnd();
                    var links = line.Split('\n');
                    return links;
                }
            }
            catch (Exception)
            {
                throw new Exception("Файл c сылками не может быть прочитан");
            }
        }
    }
}