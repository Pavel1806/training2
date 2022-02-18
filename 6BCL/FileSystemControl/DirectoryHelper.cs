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
        // TODO: Добавить комментарии к коду.
        static public void CreateDirectory(string pathTracking, TemplateElementCollection templates) // TODO: Переименовать pathTracking,
                                                                                               // Есть ходовое название, например FileSystemWatcher
                                                                                               // Можно назвать как-нибудь watchingDirectories
                                                                                               // templates тоже нужно переименовать, поскольку название
                                                                                               // ни о чём не говорит.
        {
            
            if (!Directory.Exists(pathTracking)) // TODO: Что если будет эта папка, но не будет её дочерних? Дочерние не создадуться? Замечание всё ещё актуально.
            {
                Directory.CreateDirectory(pathTracking);
                foreach (TemplateElement item in templates)
                {
                    Directory.CreateDirectory(Path.Combine(pathTracking, item.DirectoryName));
                }
            }
        }
    }
}
