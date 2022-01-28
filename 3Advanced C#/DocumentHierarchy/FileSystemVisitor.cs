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
        List<string> stack = new List<string>();
        
        public IEnumerable<string> SearchTreeOfFoldersAndFiles()
        {
            if (stack.Count == 0)
                stack.Add(Path);

            MethodOfCallingTheEvent("Начали обход дерева");

            while (stack.Count > 0)
            {
                IEnumerable<string> directories = null;
                string path = stack[0];
                stack.RemoveAt(0);
                try
                {
                    directories = Directory.GetFileSystemEntries(path);
                }
                catch
                {
                    continue;
                }
                List<string> listAddsFilteredPath = new List<string>();
                if(directories!=null)
                {
                    foreach (var item in directories)
                    {
                        stack.Add(item);
                        if(MethodForTheAlgorithm(item) == true)
                        {
                            listAddsFilteredPath.Add(item);
                            yield return item;
                        }
                    }
                }
                if(listAddsFilteredPath.Count != 0)
                {
                    Console.WriteLine("");
                    MethodOfCallingTheEventFilteredFileFinded($"Фильтрация файлов в папке {path} закончена");

                    if (MethodOfCallingTheEventFilteredDirectoryFinded($"Фильтрация папок в папке {path} закончена") == true)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Поиск остановился");
                        Console.WriteLine("Нажмите любую клавишу");
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
            //return e.Flag;
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
            //return e.Flag;
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
            //return e.Flag;
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
            //return e.Flag;
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
            //return e.Flag;
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

