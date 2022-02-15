using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystemControl
{
    // TODO: Добавить комментарии к коду.
    static class CreateDirectory // TODO: Нужно переименовать класс, это неправильно называть классы глаголами.
                                 // Если он играет просто вспомогательную роль то можно назвать DirectoryHelper,
                                 // Также здесь виден шаблон "Фабрика" (этим не стоит сейчас заморачиваться),
                                 // но если это фабричный класс, то можно назвать DirectoryFactory
    {
        // TODO: Добавить комментарии к коду.
        // Опционально: Предлагаю разделить этот метод на два других с разной ответственностью:
        // 1. Принимает на вход коллекцию путей и создаёт папки если их нет
        // 2. Статичная коллекция или метод возвращающий directoriesPaths,
        // который затем передаются в первый метод и используются в прочих процессах.
        static public Dictionary<string, string> CreateTestDirectory(string pathTracking) 
            // TODO: Желательно абстрагироваться от понимания "Test" в контексте рабочего кода, даже на учёбе
        {
            
            if (!Directory.Exists(pathTracking)) // TODO: Что если будет эта папка, но не будет её дочерних? Дочерние не создадуться?
            {
                Directory.CreateDirectory(pathTracking);
                Directory.CreateDirectory(Path.Join(pathTracking, "FolderTxtFiles")); // TODO: Path.Combine вместо Join более распространённая практика
                Directory.CreateDirectory(Path.Join(pathTracking, "FolderDocxFiles")); // TODO: Все значения вроде
                                                                                       // FolderTxtFiles, FolderDocxFiles являются "магическими"
                                                                                       // Желательно вынести их в константы
                Directory.CreateDirectory(Path.Join(pathTracking, "FolderDefaultFiles"));
            }

            Dictionary<string, string> pathDirectorys = new Dictionary<string, string> // TODO: Directories
            {
                ["pathTracking"] = pathTracking,
                ["FolderTxtFiles"] = Path.Join(pathTracking, "FolderTxtFiles"),
                ["FolderDocxFiles"] = Path.Join(pathTracking, "FolderDocxFiles"),
                ["FolderDefaultFiles"] = Path.Join(pathTracking, "FolderDefaultFiles")
            };

            return pathDirectorys;
        }
    }
}
