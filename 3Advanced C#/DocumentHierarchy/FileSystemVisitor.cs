using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentHierarchy
{
    public delegate bool AlgorithmForPathProcessing(string path); // TODO: [naming] Algorithm слишком абстрактно. p - неговоряще (это буква под ней может скрываться что угодно, но мы же знаем что здесь ожидаем?)

    public class FileSystemVisitor
    {
        private string path;   // TODO: [design] Почему свойство а не поле? Требуется пояснение.
                               // исправил на поле. Т.к. мы его используем только внутри этого класса, значит меняем на поле.
        private AlgorithmForPathProcessing filter; // TODO: [design] Почему свойство а не поле?
                                                   // исправил на поле. Т.к. мы его используем только внутри этого класса, значит меняем на поле.
        private List<string> listAddsFileOrDirectory = new List<string>(); // TODO: [design,naming] Что насчёт области видимости? Рекомендую не мешать порядок следования в коде полей, свойствв, методов. 
                                                                           // переместил

        public event EventHandler<FlagsEventArgs> EventStartTree; // TODO: [Design] этому событию действительно нужен FlagsEventArgs?   // я так понял, чтобы было единообразый вызов событий, я должен всегда с аргументами его делать
        public event EventHandler<FlagsEventArgs> EventFinishTree; // TODO: [Design] этому событию действительно нужен FlagsEventArgs?
        public event EventHandler<FlagsEventArgs> EventFileFinded;
        public event EventHandler<FlagsEventArgs> EventDirectoryFinded;
        public event EventHandler<FlagsEventArgs> EventFilteredFileFinded;
        public event EventHandler<FlagsEventArgs> EventFilteredDirectoryFinded;

        public FileSystemVisitor(string path, AlgorithmForPathProcessing filter)
        {
            this.path = path;
            this.filter = filter;  // TODO: [naming] Строго говоря это не метод а делегат. В названии писать делегат не надо, это и так из типа видно. "Метод для алгоритма", какого алгоритма? Вот это нужно расрыть. Это фильтр? Если так то почему бы его так и не назвать?
        }

        public IEnumerable<string> GetFoldersAndFiles() // TODO: [naming] Возвращается не дерево, зачем тогда его упоминать. ИМХО Get лучше.
        {
            if (listAddsFileOrDirectory.Count == 0)
                listAddsFileOrDirectory.Add(path);

            Start("Начали обход дерева");

            while (listAddsFileOrDirectory.Count > 0) 
            {
                IEnumerable<string> directoriesOrFiles = null;
                string path = listAddsFileOrDirectory[0]; // TODO: [design] Наблюдая обращение по индексу не могу не спросить почему listAddsFileOrDirectory список, может оптимальнее использовать что-то другое?
                                                          // Мне кажется можно использовать Dictionary. Но я подумал, что я обращаюсь всегда к первому элементу, значит времени на поиск будет уходить одинаково. Массив точно нельзя потому что количество всегда разное.
                listAddsFileOrDirectory.RemoveAt(0);

                if(Directory.Exists(path) == false)
                {
                    continue;
                }
                { 
                    directoriesOrFiles = Directory.GetFileSystemEntries(path);
                }
                if (directoriesOrFiles != null)
                {
                    foreach (var item in directoriesOrFiles)
                    {

                        listAddsFileOrDirectory.Add(item);
                        if(Directory.Exists(item))
                        {
                            DirectoryFinded($"Папка {item} найдена"); // TODO: [требование] DirectoryFinded - "каталог найден" выродилось в "Обход папок закончен".
                                                                      // исправил
                        }
                        else
                        {
                            FileFinded($"Файл {item} найден"); // TODO: [требование] FileFinded - "файл найден" выродилось в "Обход файлов закончен".
                                                               // исправил
                        }
                        if (filter(item)) // TODO: [многословность] Сравнивать выражение возращающее bool c true избыточно.
                                          // исправил
                        {

                            if (Directory.Exists(item))
                            {
                                if(FilteredDirectoryFinded($"Папка {item} отобрана"))
                                {
                                    Console.WriteLine("Здесь можно исключать папку из поиска. Нажмите enter");
                                    Console.ReadLine();
                                    
                                }
                            }
                            else
                            {
                                if(FilteredFileFinded($"Файл {item} отобран")) // TODO: [требование] FilteredFileFinded - "файл отобран" выродилось в "Фильтрация файлов в папке {path} закончена".
                                                                               // исправил
                                {
                                    Console.WriteLine("Здесь можно исключать файл из поиска. Нажмите enter");
                                    Console.ReadLine(); 
                                }
                            }
                            yield return item;
                        }
                        else
                        {

                        }
                    }
                }
            }
            Finish("Обход дерева закончен");
        }

        protected virtual void OnEventStartTree(FlagsEventArgs args)
        {
            EventStartTree?.Invoke(this, args); // TODO: Сокращённая форма на будущее.
                                                // Ок, понял. Тогда ниже пока не исправляю. Если что буду сюда заглядывать и смотреть что можно и так и так.
        }

        void Start(string mes) // TODO: [naming] Здесь и ниже. Как по мне этот метод избыточен (по правде сказать он заметает следы, и он мне не нравиться). Но если уж он есть то почему так называется?
        {                                               // Я читал про реализацию в таком варианте в книжке "Рихтера". Там конечно этот метод называется "SimulateNewMail"
            FlagsEventArgs e = new FlagsEventArgs(mes);
            OnEventStartTree(e);
        }

        protected virtual void OnEventFinishTree(FlagsEventArgs args)
        {
            var intermediateEvent = EventFinishTree;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        void Finish(string mes)
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

        void FileFinded(string mes)
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

        void DirectoryFinded(string mes)
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

        bool FilteredFileFinded(string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);
            OnEventFilteredFileFinded(e);
            return e.FlagToStopSearch;
        }

        protected virtual void OnEventFilteredDirectoryFinded(FlagsEventArgs args)
        {
            var intermediateEvent = EventFilteredDirectoryFinded;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        bool FilteredDirectoryFinded(string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);
            OnEventFilteredDirectoryFinded(e);
            return e.FlagToStopSearch;
        }
    }
}

