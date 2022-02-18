using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace FileSystemControl
{
    // TODO: Неиспользуемый код, ещё ни разу не видел чтобы была необходимость обьявлять делегат таким образом
    // Если нужен делегат метода который ничего не возвращает, то есть Action. Всё ещё не вижу необходимости в этом делегате )
    public delegate void MyDelegate();
    
    // TODO: Комментарии к коду, нужен комментарий к классу
    class FileControl
    {
        private string PathDirectoryTracking;
        private TemplateElementCollection Templates; 

        /// <summary>
        /// Событие создания файла
        /// </summary>
        public event EventHandler<EventArgs> CreateFile;

        /// <summary>
        /// Событие переноса фала в другую папку
        /// </summary>
        public event EventHandler<FileSystemEventArgs> TheRuleOfCoincidence;

        /// <summary>
        /// Конструктор для создания объекта 
        /// </summary>
        /// <param name="pathDirectoryTracking">Путь к прослушиваемой папке</param>
        /// <param name="templates">Шаблоны обработки</param>
        public FileControl(string pathDirectoryTracking, TemplateElementCollection templates) // TODO: 
        {
            PathDirectoryTracking = pathDirectoryTracking;
            Templates= templates;
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

            Console.ReadKey(true); // TODO: Перенести в Program.cs, класс FileControl не должен быть ответственнен за логику самой программы.

        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            OnTheRuleOfCoincidence(e);
        }
        // TODO: Пробел между методами, стиль важен )
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"переименовали файл {e.OldName}"); // TODO: Перенести в ресурсы
            Console.WriteLine($"теперь его название {e.Name}"); // TODO: Перенести в ресурсы
        }

        private void Watcher_Created(object sender, FileSystemEventArgs ev)
        {
            EventArgs e = new EventArgs(ev);

            FileInfo file = new FileInfo(ev.FullPath); // TODO: Нет никакой необходимости создавать обьект класса FileInfo

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
                            e.TimeCreate.Month.ToString() + e.TimeCreate.Year.ToString() + ev.Name); // TODO: Предлагаю использовать метод ToString у DateTime
                                                                                                    // В него можно передать формат даты и времени
                        FileInfo fileNew = new FileInfo(t); // TODO: Нет никакой необходимости создавать обьект класса FileInfo
                        if (!fileNew.Exists)
                            file.MoveTo(t);
                    }
                    else // TODO: Грубое дублирование логики
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
