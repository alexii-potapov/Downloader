using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Downloader.Models;

namespace Downloader.Utils
{
    public static class FileReader
    {
        public static ConcurrentDictionary<string, IList<string>> GetLinks(string path)
        {
            try
            {
                using (var sr = new StreamReader(path))
                {
                    var lines = sr.ReadToEnd().Replace("\r", string.Empty).Split('\n').Where(s => s != "");

                  
                    ConcurrentDictionary<string, IList<string>> links = new ConcurrentDictionary<string, IList<string>>();

                    foreach (var line in lines)
                    {
                        var linkParams = line.Split(' ');
                        var url = linkParams[0];
                        var fileName = linkParams[1];

                        links.AddOrUpdate(
                            url, 
                            (key) => new List<string> { fileName } ,
                            (key, values) =>
                                {
                                    if (!values.Contains(fileName))
                                    {
                                        values.Add(fileName);
                                    }
                                    return values;
                                });
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