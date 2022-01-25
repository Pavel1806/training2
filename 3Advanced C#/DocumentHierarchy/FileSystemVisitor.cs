using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentHierarchy
{
    public delegate bool Algorithm(string p);
    class FileSystemVisitor
    {
        string Path { get; set; }

        List<string> ListForFoldersAndFiles { get; set; }

        Algorithm MethodForTheAlgorithm { get; set; }
        MyEvent myEvent { get; set; }


        public FileSystemVisitor(string path, Algorithm methodForTheAlgorithm)
        {
            Path = path;
            MethodForTheAlgorithm = methodForTheAlgorithm;
            ListForFoldersAndFiles = new List<string>();
            myEvent = new MyEvent();
            Output output = new Output(myEvent);
        }

        public IEnumerable<string> CollectingTreeOfFoldersAndFiles()
        {

            Stack<string> stack = new Stack<string>();

            if(ListForFoldersAndFiles.Count == 0)
                ListForFoldersAndFiles.Add(Path);

            if (stack.Count == 0)
                stack.Push(Path);

            myEvent.MethodOfCallingTheEvent("Начат обход дерева");

            while (stack.Count > 0)
            {
                string path = stack.Pop();

                var directories = Directory.EnumerateDirectories(path);

                var file = Directory.EnumerateFiles(path);

                if (directories != null)
                {
                    foreach (var item in directories)
                    {
                        stack.Push(item);

                        if(MethodForTheAlgorithm(item) == true)
                        {
                            ListForFoldersAndFiles.Add(item);
                            Console.WriteLine(item);
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
                            Console.WriteLine(item);
                        }
                               
                    }
                }

                if (stack.Count == 0)
                {
                    myEvent.MethodOfCallingTheEvent("Закончен обход дерева");
                }
            }
            return ListForFoldersAndFiles;
        }
    }
}

