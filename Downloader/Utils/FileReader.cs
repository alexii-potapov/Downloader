using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Utils
{
    public static class FileReader
    {
        public static string[] GetLinks(string path)
        {
            var list = System.IO.File.ReadAllLines(path);

            return list;
        }
    }
}
