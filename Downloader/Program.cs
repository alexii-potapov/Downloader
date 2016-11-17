using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Downloader.Models;

namespace Downloader
{
    class Program
    {
        static void Main(string[] arguments)
        {
            var argumentsCheckResult = CheckParams(arguments);
            if (argumentsCheckResult!="")
            {
                Console.WriteLine(argumentsCheckResult);
                return;
            }

            //Todo 
            //Вынести парсер из конструктора  и объеденить в CheckParams
            //Сначала Парсить параметры , потом создать DownloadTask

            DownloadTask downloadTask;
            try
            {
                downloadTask = new DownloadTask(arguments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }



        }

        private static string CheckParams(string[] parameters)
        {
            if (parameters.Length%2 != 0)
            {
                return "Неверно указаны параметры";
            }
            if (parameters.Length < 8)
            {
                return "Указаны не все параметры";
            }
            return "" ;
        }
    }
}
