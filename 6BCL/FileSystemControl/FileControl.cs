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
        /// Коллекция для путей файлов
        /// </summary>
        private ConcurrentQueue<string> listObjectAndArgs = new ConcurrentQueue<string>();

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

            Timer timer = new Timer(tm, null, 0, 2000); 
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

            listObjectAndArgs.Enqueue(e.FullPath);
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
            if (listObjectAndArgs.IsEmpty)
                return;

            string fullpath = null;

            while (listObjectAndArgs.TryDequeue(out fullpath))
            {
                FileInfo fileInfo = new FileInfo(fullpath);

                var name = fileInfo.Name;
                
                var creationTime = File.GetCreationTime(fullpath);

                var template = FileTrackingTemplates.Cast<TemplateElement>()
                    .FirstOrDefault(f => Regex.IsMatch(name, f.Filter));

                if (template != null)
                {
                    string destFile = null;

                    int number = Directory.GetFiles(Path.Combine(PathDirectoryTracking, template.DirectoryName)).Length;

                    if (template.IsAddDate)
                    {
                        destFile = creationTime.ToString("d") + ".";
                    }

                    if (template.IsAddId)
                    {
                        destFile = (number + 1).ToString() + "." + destFile;
                    }

                    var sourceFile = Path.Combine(PathDirectoryTracking, name);

                    destFile = Path.Combine(PathDirectoryTracking, template.DirectoryName, destFile + name);

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
                        Console.WriteLine($"{Messages.fileNotFoundSourceFolder}");
                    }
                }

                if (template == null) // TODO: Не совсем то имел ввиду, это можно вынести наверх и внутрь добавить continue,
                                        // чтобы перейти к следующему файлу, а от if который сейчас template != null можно избавиться
                                        // Так просто будет более читаемо :)
                    Console.WriteLine($"{Messages.templateEmpty}");
            }
        }
    }
}
