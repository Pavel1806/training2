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
            CultureInfo.CurrentUICulture = new CultureInfo("en-US");

            if (Configuration.LocalizationIsAddRu())
              CultureInfo.CurrentUICulture = new CultureInfo("ru-RU");

            var pathDirectoryTracking = Path.Combine(Environment.CurrentDirectory, Configuration.FolderListenTo());

            var FileTrackingTemplates = Configuration.FileProcessingTemplates();

            DirectoryHelper.CreateDirectory(pathDirectoryTracking, FileTrackingTemplates);

            FileControl fileСontrol = new FileControl(pathDirectoryTracking, FileTrackingTemplates);

            fileСontrol.CreateFile += Fc_CreateFile;
            fileСontrol.TheRuleOfCoincidence += Fc_TheRuleOfCoincidence;
            fileСontrol.RenameFile += Fc_RenameFile;
            fileСontrol.ControlDirectory();

            Console.ReadKey(true);
        }

        private static void Fc_RenameFile(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"{Messages.oldName} {e.OldName}");
            Console.WriteLine($"{Messages.newName} {e.Name}");
        }

        private static void Fc_TheRuleOfCoincidence(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{Messages.fileMovedFolder} {e.FullPath}");
        }

        private static void Fc_CreateFile(object sender, EventArgs e)
        {
            Console.WriteLine($"{Messages.fileСreated} {e.eventArgs.Name}");
            Console.WriteLine($"{Messages.creationTime} {e.TimeCreate}");
        }
    }
}
