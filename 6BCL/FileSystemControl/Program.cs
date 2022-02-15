using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FileSystemControl
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathTracking = Path.Join(Environment.CurrentDirectory, "FolderForTrackingFiles"); // TODO: Path.Combine более распространённая практика
            Dictionary<string, string> path= CreateDirectory.CreateTestDirectory(pathTracking); // TODO: Пробел после path - стиль очень важен.

            FileControl filecontole = new FileControl(path);
            filecontole.CreateFile += Fc_CreateFile;
            filecontole.TheRuleOfCoincidence += Fc_TheRuleOfCoincidence;
            filecontole.ControlDirectory();

        }

        private static void Fc_TheRuleOfCoincidence(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Файл перенесли в папку {e.FullPath}");
        }

        private static void Fc_CreateFile(object sender, EventArgs e)
        {
            Console.WriteLine($"Файл {e.eventArgs.Name} создался в {e.TimeCreate}");
        }
    }
}
