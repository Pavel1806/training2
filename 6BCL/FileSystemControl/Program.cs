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

            var col = Configuration.DirectoryListenTo();
            string pathTracking = null;
            if (col.Count == 1)
            {
                foreach (DirectoryElement item in col)
                {
                    pathTracking = Path.Combine(Environment.CurrentDirectory, item.DirectoryName);
                }
            }

            var templates = Configuration.FileProcessingTemplates();

            DirectoryHelper.CreateDirectory(pathTracking, templates);

            ////string pathTracking = Path.Join(Environment.CurrentDirectory, "FolderForTrackingFiles"); // TODO: Path.Combine более распространённая практика
            //Dictionary<string, string> path = CreateDirectory.CreateTestDirectory(pathTracking); // TODO: Пробел после path - стиль очень важен.

            FileControl filecontrole = new FileControl(pathTracking, templates);
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
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-EN");
            Console.WriteLine($"{Messages.fileСreated} {e.eventArgs.Name}");
            Console.WriteLine($"{Messages.creationTime} {e.TimeCreate}");
        }
    }
}
