using FileSystemControl.ConfigurationProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using FileSystemControl.Resources;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace FileSystemControl
{
    /// <summary>
    /// Класс для обработки файлов
    /// </summary>
    class FileControl
    {
        /// <summary>
        /// Путь прослушиваемой папки
        /// </summary>
        private string PathDirectoryTracking;

        /// <summary>
        /// Шаблоны обработки файлов
        /// </summary>
        private TemplateElementCollection FileTrackingTemplates; 

        /// <summary>
        /// Событие создания файла
        /// </summary>
        public event EventHandler<FileSystemEventArgs> CreateFile;

        /// <summary>
        /// Событие переименования файла
        /// </summary>
        public event EventHandler<RenamedEventArgs> RenameFile;

        /// <summary>
        /// Событие переноса фала в другую папку
        /// </summary>
        public event EventHandler<FileSystemEventArgs> TheRuleOfCoincidence;

        /// <summary>
        /// Объект для обработки нескольких файлов
        /// </summary>
        private object Locker = new object(); // TODO: Нигде не используется

        /// <summary>
        /// Словарь для созданных объектов и аргументов по событию Watcher_Changed
        /// </summary>
        private ConcurrentQueue<FileSystemEventArgs> listObjectAndArgs 
            = new ConcurrentQueue<FileSystemEventArgs>(); // TODO: Подумать как не передавать FileSystemEventArgs.
                                                          // Пусть он освободит от себя память после завершения ивента.

        /// <summary>
        /// Конструктор для создания объекта 
        /// </summary>
        /// <param name="pathDirectoryTracking">Путь к прослушиваемой папке</param>
        /// <param name="fileTrackingTemplates">Шаблоны обработки файлов</param>
        public FileControl(string pathDirectoryTracking, TemplateElementCollection fileTrackingTemplates)
        {
            PathDirectoryTracking = pathDirectoryTracking;
            FileTrackingTemplates = fileTrackingTemplates;
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
        }

        public void FileProcessingMethod()
        {
            // Опционально: Можно сделать без таймера.
            TimerCallback tm = new TimerCallback(WatcherCreatedLogic);

            Timer timer = new Timer(tm, ObjectAndArgs(), 0, 2000); // TODO: Зачем передавать в качестве аргумента приватную перменную
                                                                    // которая известна всему классу?
        }
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            OnTheRuleOfCoincidence(e);
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            OnRenameFile(e);
        }

        IEnumerable<FileSystemEventArgs> ObjectAndArgs() // TODO: Неправильное название метода, метод называется существительными
                                                        // Как правило метод должен называться глаголом, поскольку является действием.
        {
            foreach (var item in listObjectAndArgs)
            {
                FileSystemEventArgs ev = item;

                yield return ev;
            }
        }
        
        private void Watcher_Created(object sender, FileSystemEventArgs e) 
        {
            OnCreateFile(e);
            listObjectAndArgs.Enqueue(e);
        }

        protected virtual void OnCreateFile(FileSystemEventArgs e)
        {
            CreateFile?.Invoke(this, e);
        }

        protected virtual void OnTheRuleOfCoincidence(FileSystemEventArgs e)
        {
            TheRuleOfCoincidence?.Invoke(this, e);
        }

        protected virtual void OnRenameFile(RenamedEventArgs e)
        {
            RenameFile?.Invoke(this, e);
        }

        public void WatcherCreatedLogic(object obj)
        {
            var list = (IEnumerable<FileSystemEventArgs>)obj; // TODO: Необязательная передача аргумента которая известна всему классу.
                                                            // TODO: Нейминг, коллекция называется list, это название ни о чём не говорит
            if (list.Count() != 0) // TODO: Необязательная проверка, более того, весь код обёрнутый в if труднее читать, чем если бы было
                                    // if (list.Count() != 0) return;
            {
                foreach (var item in list) // TODO: Нейминг, list и item не говорящие названия
                {
                    var timeCreate = File.GetCreationTime(item.FullPath); // TODO: Нейминг важен, timeCreate(-ion) и creationTime разные вещи :)

                    var template = FileTrackingTemplates.Cast<TemplateElement>()
                        .FirstOrDefault(f => Regex.IsMatch(item.Name, f.Filter));

                    if (template != null)
                    {
                        string destPathFile = null; // TODO: Нейминг важен, destPathFile и destFilePath разные вещи :)

                        int number = Directory.GetFiles(Path.Combine(PathDirectoryTracking, template.DirectoryName)).Length;

                        if (template.IsAddDate)
                        {
                            destPathFile = timeCreate.ToString("d") + ".";
                        }

                        if (template.IsAddId)
                        {
                            destPathFile = (number + 1).ToString() + "." + destPathFile;
                        }

                        var sourceFile = Path.Combine(PathDirectoryTracking, item.Name);

                        var destFile = Path.Combine(PathDirectoryTracking, template.DirectoryName, destPathFile + item.Name);
                        // TODO: Зачем вторая переменная с целевым путём? Получается 2 значения с целевым путём, можно использовать одно.

                        if (File.Exists(sourceFile))
                        {
                            if (!File.Exists(destFile))
                            {
                                File.Move(sourceFile, destFile);
                            }
                            else
                            {
                                Console.WriteLine($"{Messages.fileExists}");
                            }
                        }
                        else
                        {
                            break; // TODO: Если хоть одного файла в коллекции не будет, то процесс завершиться, почему?
                                    // Один неуспешно обработанный файл не должен влиять на работу всего процесса
                        }
                    }
                    else // TODO: можно обойтись без else, так получается "Спагетти-код", его труднее читать.
                    {
                        Console.WriteLine($"{Messages.templateEmpty}");
                    }
                }
            }
        }
    }
}
