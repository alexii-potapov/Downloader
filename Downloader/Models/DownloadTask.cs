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
        public DownloadTask(string[] arguments)
        {
            for (int i = 0; i < arguments.Length; i=i+2)
            {
                var parameter = arguments[i];
                var value = arguments[i + 1];
                switch (parameter)
                {
                    case "-n":
                        try
                        {
                        Threads = int.Parse(value);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Неврно указан параметр числа потоков"); ;
                        }
                        break;
                    case "-l":
                        try
                        {
                            LimitRate = int.Parse(value);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Неврно указан параметр ограничения по скорости"); ;
                        }
                        break;
                    case "-f":
                        Links = FileReader.GetLinks(value); ;
                        break;
                    case "-o":
                        OutputFolder = value;
                        break;
                }
            }
        }

        public int Threads { get; private set; }
        public int LimitRate { get; private set; }
        public string[] Links { get; private set; }
        public string OutputFolder { get; private set; }
    }
}
