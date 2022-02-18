using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using FileSystemControl.Resources;


namespace FileSystemControl
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Лишний пробел в коде. Стиль важен, желательно визуально разделить обьявления переменных и if
            var col = Configuration.DirectoryListenTo(); // TODO: Название col ни о чём не говорит :)
            string pathTracking = null;
            if (col.Count == 1) // TODO: Ненадёжная проверка, что если в коллекции будет больше чем 1 запись?
            {
                foreach (DirectoryElement item in col) // TODO: Что если в коллекции будет больше чем одна запись? Может нужно определить ту которая нужна
                                                        // уникальным ключём?
                {
                    pathTracking = Path.Combine(Environment.CurrentDirectory, item.DirectoryName);
                }
            }

            var templates = Configuration.FileProcessingTemplates();

            DirectoryHelper.CreateDirectory(pathTracking, templates);

            FileControl filecontrole = new FileControl(pathTracking, templates); // TODO: Нарушен "СamelСase" в названии.
            filecontrole.CreateFile += Fc_CreateFile;
            filecontrole.TheRuleOfCoincidence += Fc_TheRuleOfCoincidence;
            filecontrole.ControlDirectory();

        }

        private static void Fc_TheRuleOfCoincidence(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{Messages.fileMovedFolder} {e.FullPath}");
        }

        private static void Fc_CreateFile(object sender, EventArgs e)
        {
            //CultureInfo.CurrentCulture = new CultureInfo("en-EN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-EN"); // TODO: По условиям задачи культура должна регулироваться через конфигурацию
            Console.WriteLine($"{Messages.fileСreated} {e.eventArgs.Name}");
            Console.WriteLine($"{Messages.creationTime} {e.TimeCreate}");
        }
    }
}
