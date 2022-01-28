using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentHierarchy
{
    public delegate bool Algorithm(string p); // TODO: [naming] Algorithm слишком абстрактно. p - неговоряще (это буква под ней может скрываться что угодно, но мы же знаем что здесь ожидаем?)

    public class FileSystemVisitor
    {
        string Path { get; set; } // TODO: [design] Почему свойство а не поле? Требуется пояснение.

        Algorithm MethodForTheAlgorithm { get; set; } // TODO: [design] Почему свойство а не поле?

        public event EventHandler<FlagsEventArgs> EventStartTree; // TODO: [Design] этому событию действительно нужен FlagsEventArgs?
        public event EventHandler<FlagsEventArgs> EventFinishTree; // TODO: [Design] этому событию действительно нужен FlagsEventArgs?
        public event EventHandler<FlagsEventArgs> EventFileFinded;
        public event EventHandler<FlagsEventArgs> EventDirectoryFinded;
        public event EventHandler<FlagsEventArgs> EventFilteredFileFinded;
        public event EventHandler<FlagsEventArgs> EventFilteredDirectoryFinded;

        public FileSystemVisitor(string path, Algorithm methodForTheAlgorithm)
        {
            Path = path;
            MethodForTheAlgorithm = methodForTheAlgorithm; // TODO: [naming] Строго говоря это не метод а делегат. В названии писать делегат не надо, это и так из типа видно. "Метод для алгоритма", какого алгоритма? Вот это нужно расрыть. Это фильтр? Если так то почему бы его так и не назвать?
        }

        List<string> listAddsFileOrDirectory = new List<string>(); // TODO: [design,naming] Что насчёт области видимости? Рекомендую не мешать порядок следования в коде полей, свойствв, методов.

        public IEnumerable<string> SearchTreeOfFoldersAndFiles() // TODO: [naming] Возвращается не дерево, зачем тогда его упоминать. ИМХО Get лучше.
        {
            if (listAddsFileOrDirectory.Count == 0)
                listAddsFileOrDirectory.Add(Path);

            MethodOfCallingTheEvent("Начали обход дерева");

            while (listAddsFileOrDirectory.Count > 0) 
            {
                IEnumerable<string> directories = null;
                string path = listAddsFileOrDirectory[0]; // TODO: [design] Наблюдая обращение по индексу не могу не спросить почему listAddsFileOrDirectory список, может оптимальнее использовать что-то другое?
                listAddsFileOrDirectory.RemoveAt(0);

                try
                {
                    directories = Directory.GetFileSystemEntries(path);
                }
                catch
                {
                    continue; // TODO: [Design] Очень не очевидный способ проверить с файлом мы имеем дело или с каталогом. Как насчет Directory.Exist например?
                }

                List<string> listAddsFilteredDirectory = new List<string>(); // TODO: [design] Зачем эта коллекция?
                if (directories != null)
                {
                    foreach (var item in directories)
                    {
                        listAddsFileOrDirectory.Add(item);
                        if(MethodForTheAlgorithm(item) == true) // TODO: [многословность] Сравнивать выражение возращающее bool c true избыточно.
                        {
                            listAddsFilteredDirectory.Add(item);
                            yield return item;
                        }
                    }
                }

                if(listAddsFilteredDirectory.Count != 0)
                {
                    Console.WriteLine("");
                    MethodOfCallingTheEventFilteredFileFinded($"Фильтрация файлов в папке {path} закончена"); // TODO: [требование] FilteredFileFinded - "файл отобран" выродилось в "Фильтрация файлов в папке {path} закончена".

                    if (MethodOfCallingTheEventFilteredDirectoryFinded($"Фильтрация папок в папке {path} закончена") == true) // TODO: [требование] FilteredDirectoryFinded - "каталог отобран" выродилось в "Фильтрация папок в папке {path} закончена".
                    {
                        Console.WriteLine(""); // TODO: [design bag] Как взаимодействовать с пользователем не ответственность этого класса. Может у пользователя GUI. Есть событие для этого.
                        Console.WriteLine("Поиск остановился");
                        Console.WriteLine("Нажмите enter");
                        Console.ReadLine();
                        Console.WriteLine(("").PadRight(84, '-'));
                    }
                }
            }
            
            MethodOfCallingTheEventFileFinded("Обход файлов закончен"); // TODO: [требование] FileFinded - "файл найден" выродилось в "Обход файлов закончен".
            MethodOfCallingTheEventDirectoryFinded("Обход папок закончен"); // TODO: [требование] DirectoryFinded - "каталог найден" выродилось в "Обход папок закончен".
            MethodOfCallingTheEventFinish("Обход дерева закончен");
        }

        protected virtual void OnEventStartTree(FlagsEventArgs args)
        {
            EventStartTree?.Invoke(this, args); // TODO: Сокращённая форма на будущее.
        }

        public void MethodOfCallingTheEvent(string mes) // TODO: [naming] Здесь и ниже. Как по мне этот метод избыточен (по правде сказать он заметает следы, и он мне не нравиться). Но если уж он есть то почему так называется?
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);
            OnEventStartTree(e);
        }

        protected virtual void OnEventFinishTree(FlagsEventArgs args)
        {
            var intermediateEvent = EventFinishTree;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        public void MethodOfCallingTheEventFinish(string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);
            OnEventFinishTree(e);
        }

        protected virtual void OnEventFileFinded(FlagsEventArgs args)
        {
            var intermediateEvent = EventFileFinded;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        public void MethodOfCallingTheEventFileFinded(string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);
            OnEventFileFinded(e);
        }

        protected virtual void OnEventDirectoryFinded(FlagsEventArgs args)
        {
            var intermediateEvent = EventDirectoryFinded;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        public void MethodOfCallingTheEventDirectoryFinded(string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);
            OnEventDirectoryFinded(e);
        }

        protected virtual void OnEventFilteredFileFinded(FlagsEventArgs args)
        {
            var intermediateEvent = EventFilteredFileFinded;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        public void MethodOfCallingTheEventFilteredFileFinded(string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);
            OnEventFilteredFileFinded(e);
        }

        protected virtual void OnEventFilteredDirectoryFinded(FlagsEventArgs args)
        {
            var intermediateEvent = EventFilteredDirectoryFinded;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        public bool MethodOfCallingTheEventFilteredDirectoryFinded(string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);
            OnEventFilteredDirectoryFinded(e);
            return e.FlagToStopSearch;
        }
    }
}

