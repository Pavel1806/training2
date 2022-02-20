using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using FileSystemControl.ConfigurationProject;
using FileSystemControl.Resources;
using System.Linq;
using System.Linq.Expressions;


namespace FileSystemControl
{
    class Program
    {
        static void Main(string[] args)
        {

            var configuration = (ConfigurationProjectDataSection)ConfigurationManager.GetSection("projectDataSection");

            if (configuration.Localization.IsAddEn)
              CultureInfo.CurrentUICulture = new CultureInfo("en-US");

            var pathDirectoryTracking = Path.Combine(Environment.CurrentDirectory, configuration.FolderListen.FolderListen);

            var fileTrackingTemplates = configuration.FileTrackingTemplates;

            DirectoryHelper.CreateDirectory(pathDirectoryTracking, fileTrackingTemplates);

            FileControl fileСontrol = new FileControl(pathDirectoryTracking, fileTrackingTemplates);

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
