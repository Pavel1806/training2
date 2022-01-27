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

        List<string> ListForFoldersAndFiles { get; set; }

        Algorithm MethodForTheAlgorithm { get; set; }
        
        public event EventHandler<FlagsEventArgs> EventForNotifications;


        public FileSystemVisitor(string path, Algorithm methodForTheAlgorithm)
        {
            Path = path;
            MethodForTheAlgorithm = methodForTheAlgorithm;
            ListForFoldersAndFiles = new List<string>();
            
        }

        IEnumerable<string> SearchTreeOfFoldersAndFiles()
        {
            
            Stack<string> stack = new Stack<string>();

            if(ListForFoldersAndFiles.Count == 0)
                ListForFoldersAndFiles.Add(Path);

            if (stack.Count == 0)
                stack.Push(Path);

            MethodOfCallingTheEvent("Начат обход дерева");

            while (stack.Count > 0)
            {
                string path = stack.Pop();
                IEnumerable<string> directories = null;
                try
                {
                    directories = Directory.EnumerateDirectories(path);
                }
                catch
                {
                    continue;
                }

                var file = Directory.EnumerateFiles(path);

                if (directories != null)
                {
                    foreach (var item in directories)
                    {
                        stack.Push(item);

                        if(MethodForTheAlgorithm(item) == true)
                        {
                            ListForFoldersAndFiles.Add(item);
                            //Console.WriteLine(item);
                        }    
                               
                    }
                }                
                if (file != null)
                {
                    foreach (var item in file)
                    {
                        if(MethodForTheAlgorithm(item) == true)
                        {
                            ListForFoldersAndFiles.Add(item);
                            //Console.WriteLine(item);
                        }
                    }
                }
                if (stack.Count == 0)
                {
                    MethodOfCallingTheEvent("Закончена фильтрация файлов");
                    MethodOfCallingTheEvent("Закончена фильтрация папок");
                    MethodOfCallingTheEvent("Закончен обход файлов");
                    MethodOfCallingTheEvent("Закончен обход папок");
                    MethodOfCallingTheEvent("Закончен обход дерева");
                    
                }
            }
            return ListForFoldersAndFiles;
        }
        public IEnumerable<string> CollectingTreeOfFoldersAndFiles()
        {
            foreach (var item in SearchTreeOfFoldersAndFiles())
            {
                yield return item;
            }
        }

        protected virtual void OnMyEvent(FlagsEventArgs args)
        {
            var intermediateEvent = EventForNotifications;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        public void MethodOfCallingTheEvent(string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);

            OnMyEvent(e);
        }
    }
}

