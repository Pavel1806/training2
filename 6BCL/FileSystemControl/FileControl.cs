using FileSystemControl.ConfigurationProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using System.Linq.Expressions;

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
        public event EventHandler<EventArgs> CreateFile;

        /// <summary>
        /// Событие переименования файла
        /// </summary>
        public event EventHandler<RenamedEventArgs> RenameFile;

        /// <summary>
        /// Событие переноса фала в другую папку
        /// </summary>
        public event EventHandler<FileSystemEventArgs> TheRuleOfCoincidence;

        /// <summary>
        /// Конструктор для создания объекта 
        /// </summary>
        /// <param name="pathDirectoryTracking">Путь к прослушиваемой папке</param>
        /// <param name="fileTrackingTemplates">Шаблоны обработки файлов</param>
        public FileControl(string pathDirectoryTracking, TemplateElementCollection fileTrackingTemplates) // TODO: 
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

        private void Watcher_Created(object sender, FileSystemEventArgs ev)
        {
            EventArgs e = new EventArgs(ev);

            e.TimeCreate = File.GetCreationTime(ev.FullPath);

            OnCreateFile(e);
            
            var template = FileTrackingTemplates.Cast<TemplateElement>().Where(f => Regex.IsMatch(ev.Name, f.Filter)).FirstOrDefault();

                    string destPathFile = null;

                    int number = Directory.GetFiles(Path.Combine(PathDirectoryTracking, template.DirectoryName)).Length;

                    if (template.IsAddDate)
                    {
                        destPathFile = e.TimeCreate.ToString("d") + ".";
                    }

                    if (template.IsAddId)
                    {
                        destPathFile = (number + 1).ToString() + "." + destPathFile;
                    }

                    var sourceFile = Path.Combine(PathDirectoryTracking, ev.Name);

                    var destFile = Path.Combine(PathDirectoryTracking, template.DirectoryName, destPathFile + ev.Name);

                    if (!File.Exists(destFile))
                    {
                        File.Move(sourceFile, destFile);
                    }
                    else
                    {
                        Console.WriteLine("Файл с таким именем существует");
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

        protected virtual void OnRenameFile(RenamedEventArgs e)
        {
            RenameFile?.Invoke(this, e);
        }

    }
}
