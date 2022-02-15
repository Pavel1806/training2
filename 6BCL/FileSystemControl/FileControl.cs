using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FileSystemControl
{
    public delegate void MyDelegate();
    
    class FileControl
    {
        private string PathTracking;
        private string FolderTxtFiles;
        private string FolderDocxFiles;
        private string FolderDefaultFiles;

        public event EventHandler<EventArgs> CreateFile;
        public event EventHandler<FileSystemEventArgs> TheRuleOfCoincidence;

        public FileControl(Dictionary<string, string> path)
        {
            PathTracking = path["pathTracking"];
            FolderTxtFiles = path["FolderTxtFiles"];
            FolderDocxFiles = path["FolderDocxFiles"];
            FolderDefaultFiles = path["FolderDefaultFiles"];
        }

        public void ControlDirectory()
        {
            var watcher = new FileSystemWatcher(PathTracking);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                   | NotifyFilters.CreationTime
                                   | NotifyFilters.DirectoryName
                                   | NotifyFilters.FileName
                                   | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.Security
                                   | NotifyFilters.Size;

            
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
            watcher.EnableRaisingEvents = true;

            Thread.Sleep(20*1000);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            OnTheRuleOfCoincidence(e);
        }
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            
            Console.WriteLine($"переименовали файл {e.OldName}");
            Console.WriteLine($"теперь его название {e.Name}");
        }

        private void Watcher_Created(object sender, FileSystemEventArgs ev)
        {
            EventArgs e = new EventArgs(ev);

            FileInfo file = new FileInfo(ev.FullPath);
            e.TimeCreate = file.CreationTime;

            OnCreateFile(e);

            if (file.Extension == ".txt")
            {
                file.MoveTo(Path.Join(FolderTxtFiles, ev.Name));
                
            }
            else if(file.Extension == ".docx")

            {
                file.MoveTo(Path.Join(FolderDocxFiles, ev.Name));
            }
            else
            {
                file.MoveTo(Path.Join(FolderDefaultFiles, ev.Name));
            }
        }

        protected virtual void OnCreateFile(EventArgs e)
        {
            CreateFile?.Invoke(this, e);
        }

        protected virtual void OnTheRuleOfCoincidence(FileSystemEventArgs e)
        {
            TheRuleOfCoincidence?.Invoke(this, e);
        }

    }
}
