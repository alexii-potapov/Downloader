using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Downloader.Models;

namespace Downloader.Utils
{
    public static class FileReader
    {
        public static IList<Link> GetLinks(string path)
        {
            try
            {
                using (var sr = new StreamReader(path))
                {
                    //Todo
                    //Удалить пустые строки
                    var lines = sr.ReadToEnd().Replace("\r", string.Empty).Split('\n').Where(s => s != "");

                    IList<Link> links = new List<Link>();
                    foreach (var line in lines)
                    {
                        var linkParams = line.Split(' ');
                        links.Add(new Link(linkParams[0], linkParams[1]));
                    }
                    return links;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("Файл c сылками не может быть прочитан");
            }
        }
    }
}