using System;
using System.IO;
using System.Linq;

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
                    //Todo
                    //Удалить пустые строки
                    var line = sr.ReadToEnd().Replace("\r", String.Empty); 
                    var links = line.Split('\n').Where((s => s!="")).ToArray();
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