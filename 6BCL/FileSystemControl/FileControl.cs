using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

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

        /// <summary> TODO: Комментарий :)
        /// 
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

            foreach (TemplateElement item in FileTrackingTemplates) // TODO: Изменить поиск подходящего шаблона на выражение Linq
            {
                var sourceFile = Path.Combine(PathDirectoryTracking, ev.Name); // TODO: Получается мы будем создавать огромное количество ненужных переменных
                                                                                // до момента пока не найдём подходящий шаблон. Получается лишняя логика ==
                                                                                // излишняя нагрузка на процессор и оперативную память.

                var destPath = Path.Combine(PathDirectoryTracking, item.DirectoryName);

                var destFileDate = Path.Combine(destPath, e.TimeCreate.ToString("d") + ev.Name);

                int number = Directory.GetFiles(Path.Combine(PathDirectoryTracking, item.DirectoryName)).Length;

                var destFileId = Path.Combine(destPath, (number + 1).ToString() + ev.Name);

                var destFileDateAndId = Path.Combine(destPath, (number + 1).ToString()+ "." + e.TimeCreate.ToString("d") + ev.Name);

                var destFile = Path.Combine(destPath, ev.Name);

                if (Regex.IsMatch(ev.Name, item.Filter, RegexOptions.IgnoreCase)) // TODO: см условие выше
                {
                    if (!File.Exists(destFileDate))
                    {
                        // TODO: Всю логику ниже и выше необходимо упростить.
                        // Это очень плохой подход. Почему бы не делать так?:
                        // 1. Находим к какому шаблону подходит файл
                        // 2. Проверяем, нужно ли добавить к финальному имени дату?:
                        //   a. Да - добавляем дату к итоговому имени
                        //   b. Нет - Не добавляем дату к итоговому имени, оставляем как есть
                        //   ... Дальше додумать самостоятельно.
                        if (item.IsAddDate && item.IsAddId)
                        {
                            File.Move(sourceFile, destFileDateAndId);
                        }

                        if (item.IsAddDate && !item.IsAddId)
                        {
                            File.Move(sourceFile, destFileDate);
                        }

                        if (!item.IsAddId && item.IsAddId)
                        {
                            File.Move(sourceFile, destFileId);
                        }

                        if (!item.IsAddDate && !item.IsAddId)
                        {
                            File.Move(sourceFile, destFile);
                        }
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

        protected virtual void OnRenameFile(RenamedEventArgs e)
        {
            RenameFile?.Invoke(this, e);
        }

    }
}
