using System;
using System.Collections.Generic;
using Downloader.Models;

namespace Downloader.Utils
{
    public static class AgumentParser
    {
        public static DownloadTask ParseParams(string[] parameters)
        {
            if (parameters.Length % 2 != 0)
            {
                throw new Exception("Неверно указаны параметры");
            }
            if (parameters.Length < 8)
            {
                throw new Exception("Указаны не все параметры");
            }

            var threadCount = 0;
            var limitRate = 0;
            IList<Link> links = new List<Link>();
            var outputFolder = "";


            for (var i = 0; i < parameters.Length; i = i + 2)
            {
                var parameter = parameters[i];
                var value = parameters[i + 1];

                switch (parameter)
                {
                    case "-n":
                        try
                        {
                            threadCount = int.Parse(value);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Неврно указан параметр числа потоков");
                        }
                        break;
                    case "-l":
                        try
                        {
                            limitRate = int.Parse(value);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Неврно указан параметр ограничения по скорости");
                        }
                        break;
                    case "-f":
                        links = FileReader.GetLinks(value);
                        break;
                    case "-o":
                        outputFolder = value;
                        break;
                }
            }

            return new DownloadTask(threadCount, limitRate, links, outputFolder);
        }
    }
}
