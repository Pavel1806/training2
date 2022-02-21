using FileSystemControl.ConfigurationProject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс для проверки и создания папок для проекта
    /// </summary>
    static class DirectoryHelper
    {
        /// <summary>
        /// Метод проверки или создания папок для проекта
        /// </summary>
        /// <param name="pathDirectoryTracking">Отслеживаемая папка</param>
        /// <param name="fileTrackingTemplates">Шаблоны обработки файлов</param>
        public static void CreateDirectories(string pathDirectoryTracking, TemplateElementCollection fileTrackingTemplates)
        {

            if (!Directory.Exists(pathDirectoryTracking))
            {
                Directory.CreateDirectory(pathDirectoryTracking);
            }
            foreach (TemplateElement item in fileTrackingTemplates)
            {
                if(!Directory.Exists(Path.Combine(pathDirectoryTracking, item.DirectoryName)))
                  Directory.CreateDirectory(Path.Combine(pathDirectoryTracking, item.DirectoryName));
            }
        }
    }
}
