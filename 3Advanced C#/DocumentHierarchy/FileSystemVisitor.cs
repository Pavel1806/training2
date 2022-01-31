using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentHierarchy
{
    public delegate bool AlgorithmForPathProcessing(string path);

    public class FileSystemVisitor
    {
        private string path;
        private AlgorithmForPathProcessing filter;
        private Queue<string> listAddsFileOrDirectory = new Queue<string>(); 

        public event EventHandler<FlagsEventArgs> EventStartTree; // TODO: [Design] этому событию действительно нужен FlagsEventArgs?   // я так понял, чтобы было единообразый вызов событий, я должен всегда с аргументами его делать
        public event EventHandler<FlagsEventArgs> EventFinishTree; // TODO: [Design] этому событию действительно нужен FlagsEventArgs?
        public event EventHandler<FlagsEventArgs> EventFileFinded;
        public event EventHandler<FlagsEventArgs> EventDirectoryFinded;
        public event EventHandler<FlagsEventArgs> EventFilteredFileFinded;
        public event EventHandler<FlagsEventArgs> EventFilteredDirectoryFinded;

        public FileSystemVisitor(string path, AlgorithmForPathProcessing filter)
        {
            this.path = path;
            this.filter = filter;  
        }

        public IEnumerable<string> GetFoldersAndFiles()
        {
            if (listAddsFileOrDirectory.Count == 0)
                listAddsFileOrDirectory.Enqueue(path);

            Start();

            int numberProcessedFoldersOrFiles = 0;
            bool endSearch = false;

            while (listAddsFileOrDirectory.Count > 0) 
            {
                IEnumerable<string> directoriesOrFiles = null;
                string path = listAddsFileOrDirectory.Dequeue(); // TODO: [design] Наблюдая обращение по индексу не могу не спросить почему listAddsFileOrDirectory список, может оптимальнее использовать что-то другое?
                                                          // Мне кажется можно использовать Dictionary. Но я подумал, что я обращаюсь всегда к первому элементу, значит времени на поиск будет уходить одинаково. Массив точно нельзя потому что количество всегда разное.
                //listAddsFileOrDirectory.RemoveAt(0);
                if (Directory.Exists(path) == false)
                {
                    continue;
                    //directories = Directory.GetFileSystemEntries(path); // TODO: Должен признать что реализация с EnumerateDirectories и EnumerateFiles получилось бы лучше. Должен признать мой совет с GetFileSystemEntries оказался плохим. Проверки на то что получили мы файл или каталог не делают код лучше.
                }
                { 
                    directoriesOrFiles = Directory.GetFileSystemEntries(path);
                }

                if (directoriesOrFiles != null)
                {
                    foreach (var item in directoriesOrFiles)
                    {
                        listAddsFileOrDirectory.Enqueue(item);
                        if(Directory.Exists(item))
                        {
                            DirectoryFinded(item); 
                        }
                        else
                        {
                            FileFinded(item);
                        }
                        if (filter(item)) 
                        {
                            
                            if (Directory.Exists(item))
                            {
                                if(FilteredDirectoryFinded(item))
                                {
                                    
                                }
                            }
                            else
                            {
                                numberProcessedFoldersOrFiles++;
                                if (FilteredFileFinded(item, numberProcessedFoldersOrFiles))
                                {
                                    endSearch = true;
                                    yield return item;
                                    break;
                                }
                            }
                            yield return item;
                        }
                    }
                }
                if(endSearch)
                {
                    break;
                }
            }
            Finish();
        }

        protected virtual void OnEventStartTree(FlagsEventArgs args)
        {
            EventStartTree?.Invoke(this, args); // TODO: Сокращённая форма на будущее.
            //var intermediateEvent = EventStartTree;
            //if (intermediateEvent != null)
            //    intermediateEvent(this, args);
        }
        void Start()
        {
            FlagsEventArgs e = new FlagsEventArgs();
            OnEventStartTree(e);
        }

        protected virtual void OnEventFinishTree(FlagsEventArgs args)
        {
            var intermediateEvent = EventFinishTree;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        void Finish()
        {
            FlagsEventArgs e = new FlagsEventArgs();
            OnEventFinishTree(e);
        }

        protected virtual void OnEventFileFinded(FlagsEventArgs args)
        {
            var intermediateEvent = EventFileFinded;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        void FileFinded(string name)
        {
            FlagsEventArgs e = new FlagsEventArgs();
            e.Name = name;
            OnEventFileFinded(e);
        }

        protected virtual void OnEventDirectoryFinded(FlagsEventArgs args)
        {
            var intermediateEvent = EventDirectoryFinded;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        void DirectoryFinded(string name)
        {
            FlagsEventArgs e = new FlagsEventArgs();
            e.Name = name;
            OnEventDirectoryFinded(e);
        }

        protected virtual void OnEventFilteredFileFinded(FlagsEventArgs args)
        {
            var intermediateEvent = EventFilteredFileFinded;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        bool FilteredFileFinded(string name, int number)
        {
            FlagsEventArgs e = new FlagsEventArgs();
            e.Name = name;
            e.NumberOfFoldersOrFilesProcessed = number;
            OnEventFilteredFileFinded(e);
            return e.FlagToStopSearch;
        }

        protected virtual void OnEventFilteredDirectoryFinded(FlagsEventArgs args)
        {
            var intermediateEvent = EventFilteredDirectoryFinded;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        bool FilteredDirectoryFinded(string name)
        {
            FlagsEventArgs e = new FlagsEventArgs();
            e.Name = name;
            OnEventFilteredDirectoryFinded(e);
            return e.FlagToStopSearch;
        }
    }
}

