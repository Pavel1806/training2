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
        private object Locker = new object();

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

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            OnTheRuleOfCoincidence(e);
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            OnRenameFile(e);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e) 
        {
            OnCreateFile(e);
            Action<object, FileSystemEventArgs> LogiсAction;
            LogiсAction = WatcherCreatedLogic; 
            LogiсAction(sender, e);
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

        private void WatcherCreatedLogic(object sender, FileSystemEventArgs e)
        {
            lock (Locker)
            {
                var timeCreate = File.GetCreationTime(e.FullPath);

                var template = FileTrackingTemplates.Cast<TemplateElement>()
                    .FirstOrDefault(f => Regex.IsMatch(e.Name, f.Filter));

                if (template != null)
                {
                    string destPathFile = null;

                    int number = Directory.GetFiles(Path.Combine(PathDirectoryTracking, template.DirectoryName)).Length;

                    if (template.IsAddDate)
                    {
                        destPathFile = timeCreate.ToString("d") + ".";
                    }

                    if (template.IsAddId)
                    {
                        destPathFile = (number + 1).ToString() + "." + destPathFile;
                    }

                    var sourceFile = Path.Combine(PathDirectoryTracking, e.Name);

                    var destFile = Path.Combine(PathDirectoryTracking, template.DirectoryName, destPathFile + e.Name);

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
                    Console.WriteLine($"{Messages.templateEmpty}");
                }
            }
        }
    }
}
