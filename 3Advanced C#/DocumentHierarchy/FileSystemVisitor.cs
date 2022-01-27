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
        List<string> stack = new List<string>();
        
        public IEnumerable<string> SearchTreeOfFoldersAndFiles()
        {
            if (stack.Count == 0)
                stack.Add(Path);
            
            while(stack.Count > 0)
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
                if(directories!=null)
                {
                    foreach (var item in directories)
                    {
                        stack.Add(item);
                        if(MethodForTheAlgorithm(item) == true)
                         yield return item;
                    }
                }
                
            }
        }
       
        protected virtual void OnMyEvent(FlagsEventArgs args)
        {
            var intermediateEvent = EventForNotifications;
            if (intermediateEvent != null)
                intermediateEvent(this, args);
        }

        public bool MethodOfCallingTheEvent(string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);

            OnMyEvent(e);
            return e.Flag;
        }

    }
}

