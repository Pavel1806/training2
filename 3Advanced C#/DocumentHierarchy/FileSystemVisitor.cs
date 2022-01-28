using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentHierarchy
{
    public delegate bool Algorithm(string p);
    public class FileSystemVisitor
    {
        string Path { get; set; }

        Algorithm MethodForTheAlgorithm { get; set; }
        
        public event EventHandler<FlagsEventArgs> EventStartTree;
        public event EventHandler<FlagsEventArgs> EventFinishTree;
        public event EventHandler<FlagsEventArgs> EventFileFinded;
        public event EventHandler<FlagsEventArgs> EventDirectoryFinded;
        public event EventHandler<FlagsEventArgs> EventFilteredFileFinded;
        public event EventHandler<FlagsEventArgs> EventFilteredDirectoryFinded;

        public FileSystemVisitor(string path, Algorithm methodForTheAlgorithm)
        {
            Path = path;
            MethodForTheAlgorithm = methodForTheAlgorithm;
        }
        List<string> listAddsFileOrDirectory = new List<string>();
        
        public IEnumerable<string> SearchTreeOfFoldersAndFiles()
        {
            if (listAddsFileOrDirectory.Count == 0)
                listAddsFileOrDirectory.Add(Path);

            MethodOfCallingTheEvent("Начали обход дерева");

            while (listAddsFileOrDirectory.Count > 0)
            {
                IEnumerable<string> directories = null;
                string path = listAddsFileOrDirectory[0];
                listAddsFileOrDirectory.RemoveAt(0);
                try
                {
                    directories = Directory.GetFileSystemEntries(path);
                }
                catch
                {
                    continue;
                }
                List<string> listAddsFilteredDirectory = new List<string>();
                if(directories!=null)
                {
                    foreach (var item in directories)
                    {
                        listAddsFileOrDirectory.Add(item);
                        if(MethodForTheAlgorithm(item) == true)
                        {
                            listAddsFilteredDirectory.Add(item);
                            yield return item;
                        }
                    }
                }
                if(listAddsFilteredDirectory.Count != 0)
                {
                    Console.WriteLine("");
                    MethodOfCallingTheEventFilteredFileFinded($"Фильтрация файлов в папке {path} закончена");

                    if (MethodOfCallingTheEventFilteredDirectoryFinded($"Фильтрация папок в папке {path} закончена") == true)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Поиск остановился");
                        Console.WriteLine("Нажмите enter");
                        Console.ReadLine();
                        Console.WriteLine(("").PadRight(84, '-'));
                    }
                }
            }
            
            MethodOfCallingTheEventFileFinded("Обход файлов закончен");
            MethodOfCallingTheEventDirectoryFinded("Обход папок закончен");
            MethodOfCallingTheEventFinish("Обход дерева закончен");
        }
       
        protected virtual void OnEventStartTree(FlagsEventArgs args)
        {
            var intermediateEvent = EventStartTree;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }
        public void MethodOfCallingTheEvent(string mes)
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

