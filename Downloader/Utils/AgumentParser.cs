using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            int threadCount=0;
            long limitRate=0;
            ConcurrentDictionary<string, IList<string>> links = new ConcurrentDictionary<string, IList<string>>();
            var outputFolder = "";


            for (var i = 0; i < parameters.Length; i = i + 2)
            {
                var parameter = parameters[i];
                var value = parameters[i + 1];

                switch (parameter)
                {
                    case "-n":
                        threadCount = ParseThreadCount(value);
                        break;
                    case "-l":
                        limitRate = ParseLimitRate(value);
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

        private static int ParseThreadCount(string value)
        {
            int threadCount;
            try
            {
                threadCount = int.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception("Неверно указан параметр числа потоков");
            }
            return threadCount;
        }

        private static long ParseLimitRate(string value)
        {
            var pattern = @"(\d+)([km]?$)";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            var match = rgx.Match(value);
            if (!match.Success)
            {
                throw new Exception("Неверно указан параметр ограничения по скорости");
            }

            var limitRate = long.Parse(match.Groups[1].Value);

            if (match.Groups.Count > 2)
            {
                var suffix = match.Groups[2].Value;
                switch (suffix.ToLower())
                {
                    case "k":
                        limitRate = limitRate*1024;
                        break;
                    case "m":
                        limitRate = limitRate*1024*1024;
                        break;
                }
            }

            return limitRate;
        }
    }
}
