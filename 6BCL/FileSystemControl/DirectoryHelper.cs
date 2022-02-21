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
        static public void CreateDirectory(string pathDirectoryTracking, TemplateElementCollection fileTrackingTemplates) // TODO: Метод создаёт много папок, но
                                                                                                                            // в названии "Directory"
        {

            if (!Directory.Exists(pathDirectoryTracking))
            {
                Directory.CreateDirectory(pathDirectoryTracking);
                foreach (TemplateElement item in fileTrackingTemplates)
                {
                    Directory.CreateDirectory(Path.Combine(pathDirectoryTracking, item.DirectoryName));
                }
            }
            foreach (TemplateElement item in fileTrackingTemplates) // TODO: Дублирование логики необходимо исправить. :)
                                                                    // По хорошему метод должен принимать коллекцию путей вроде "params string[] directoriesPaths"
                                                                    // И в один foreach создавать их.
                                                                    // В текущей реализации выходит, что мы сначала создаём корневую папку, затем создаём вложенные
                                                                    // Выходим из if'а и снова пытаемся создавать вложенные.
            {
                if(!Directory.Exists(Path.Combine(pathDirectoryTracking, item.DirectoryName)))
                  Directory.CreateDirectory(Path.Combine(pathDirectoryTracking, item.DirectoryName));
            }

        }
    }
}
