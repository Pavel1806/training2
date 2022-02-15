using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FileSystemControl
{
    // TODO: Неиспользуемый код, ещё ни разу не видел чтобы была необходимость обьявлять делегат таким образом
    // Если нужен делегат метода который ничего не возвращает, то есть Action
    public delegate void MyDelegate();
    
    // TODO: Комментарии к коду
    class FileControl
    {
        private string PathTracking;
        private string FolderTxtFiles;
        private string FolderDocxFiles;
        private string FolderDefaultFiles;
        // TODO: Комментарии к коду
        public event EventHandler<EventArgs> CreateFile;
        // TODO: Комментарии к коду
        public event EventHandler<FileSystemEventArgs> TheRuleOfCoincidence;

        // TODO: Комментарии к коду
        public FileControl(Dictionary<string, string> path) // TODO: Неправильно название, коллекция содержит множество путей, а не один,
                                                            // потому логичнее назвать её paths, кроме того уточнить что это за пути
                                                            // directoriesPaths
        {
            PathTracking = path["pathTracking"]; // TODO: "магические" значения которые используются много где в коде, лучше вынести в класс с константами
                                                // "магиеческие" значения в этом случае: pathTracking, FolderTxtFiles и другие два.
                                                // TODO: Кроме того, стиль их написания не должен отличаться, например pathTracking начинается с маленькой,
                                                // а FolderTxtFiles с большой
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

            Thread.Sleep(20*1000); // TODO: "Магические" числа нужно вынести в класс с константами, но тут ещё другое
                                    // Когда поток проснётся, приложение завершит работу. Важно понимать, что FileSystemWatcher наблюдает в своём потоке
                                    // За папкой. Тем временем, можно в Program.cs написать что-то вроде
                                    // Console.Write("Нажмите любую клавишу для завершения программы:");
                                    // Console.ReadKey(true)
                                    // И получается что "Главный" поток, по завершению которого завершается работа программы
                                    // Будет висеть на курке клавиатуры пока поток FileSystemWatcher'а будет фоном следать за папкой.
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
