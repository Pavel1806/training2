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
        /// <param name="FileTrackingTemplates">Шаблоны обработки файлов</param>
        static public void CreateDirectory(string pathDirectoryTracking, TemplateElementCollection FileTrackingTemplates)
        {

            if (!Directory.Exists(pathDirectoryTracking))
            {
                Directory.CreateDirectory(pathDirectoryTracking);
                foreach (TemplateElement item in FileTrackingTemplates)
                {
                    Directory.CreateDirectory(Path.Combine(pathDirectoryTracking, item.DirectoryName));
                }
            }
            else
            {
                foreach (TemplateElement item in FileTrackingTemplates)
                {
                    if(!Directory.Exists(Path.Combine(pathDirectoryTracking, item.DirectoryName)))
                      Directory.CreateDirectory(Path.Combine(pathDirectoryTracking, item.DirectoryName));
                }
            }
        }
    }
}
