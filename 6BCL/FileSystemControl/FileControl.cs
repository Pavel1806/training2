using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace FileSystemControl
{
    // TODO: Неиспользуемый код, ещё ни разу не видел чтобы была необходимость обьявлять делегат таким образом
    // Если нужен делегат метода который ничего не возвращает, то есть Action
    public delegate void MyDelegate();
    
    // TODO: Комментарии к коду
    class FileControl
    {
        private string PathDirectoryTracking;
        private TemplateElementCollection Templates;

        // TODO: Комментарии к коду
        /// <summary>
        /// Событие создания файла
        /// </summary>
        public event EventHandler<EventArgs> CreateFile;
        // TODO: Комментарии к коду
        /// <summary>
        /// Событие переноса фала в другую папку
        /// </summary>
        public event EventHandler<FileSystemEventArgs> TheRuleOfCoincidence;

        // TODO: Комментарии к коду
        /// <summary>
        /// Конструктор для создания объекта 
        /// </summary>
        /// <param name="pathDirectoryTracking">Путь к прослушиваемой папке</param>
        /// <param name="templates">Шаблоны обработки</param>
        public FileControl(string pathDirectoryTracking, TemplateElementCollection templates) // TODO: Неправильно название, коллекция содержит множество путей, а не один,
                                                            // потому логичнее назвать её paths, кроме того уточнить что это за пути
                                                            // directoriesPaths
        {
            PathDirectoryTracking = pathDirectoryTracking; // TODO: "магические" значения которые используются много где в коде, лучше вынести в класс с константами
            Templates= templates;                              // "магиеческие" значения в этом случае: pathTracking, FolderTxtFiles и другие два.
                                                                    // TODO: Кроме того, стиль их написания не должен отличаться, например pathTracking начинается с маленькой,
                                                                    // а FolderTxtFiles с большой
        }

        public void ControlDirectory()
        {
            var watcher = new FileSystemWatcher(PathDirectoryTracking);

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

            //Thread.Sleep(20*1000); // TODO: "Магические" числа нужно вынести в класс с константами, но тут ещё другое
                                    // Когда поток проснётся, приложение завершит работу. Важно понимать, что FileSystemWatcher наблюдает в своём потоке
                                    // За папкой. Тем временем, можно в Program.cs написать что-то вроде
                                    // Console.Write("Нажмите любую клавишу для завершения программы:");
                                    // Console.ReadKey(true)
                                    // И получается что "Главный" поток, по завершению которого завершается работа программы
                                    // Будет висеть на курке клавиатуры пока поток FileSystemWatcher'а будет фоном следать за папкой.
            Console.ReadKey(true);

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

            foreach (TemplateElement item in Templates)
            {

                if (Regex.IsMatch(file.Name, item.Filter, RegexOptions.IgnoreCase))
                {
                    if (item.DateOrNumberTrue)
                    {
                        var path = Path.Combine(PathDirectoryTracking, item.DirectoryName);
                        var t = Path.Combine(path,
                            e.TimeCreate.Second.ToString() + e.TimeCreate.Day.ToString() +
                            e.TimeCreate.Month.ToString() + e.TimeCreate.Year.ToString() + ev.Name);
                        FileInfo fileNew = new FileInfo(t);
                        if (!fileNew.Exists)
                            file.MoveTo(t);
                    }
                    else
                    {
                        var path = Path.Combine(PathDirectoryTracking, item.DirectoryName);
                        int number = Directory.GetFiles(Path.Combine(PathDirectoryTracking, item.DirectoryName)).Length;
                        var t = Path.Combine(path,
                            (number + 1).ToString() + ev.Name);
                        FileInfo fileNew = new FileInfo(t);
                        if (!fileNew.Exists)
                            file.MoveTo(t);
                    }
                    break;
                }
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
