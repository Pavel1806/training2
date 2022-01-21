using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentHierarchy
{
    class FileSystemVisitor
    {
     
        string Puth { get; set; }
        string Word { get; set; }

        public FileSystemVisitor(string puth, string word)
        {
            Puth = puth;
            Word = word;
        }

        public List<string> ListDocuments()
        {

            List<string> vs = new List<string>();

            Stack<string> stack = new Stack<string>();
            
            vs.Add(Puth);

            stack.Push(Puth);

            while(stack.Count > 0)
            {
                string puth = stack.Pop();

                var directories = Directory.EnumerateDirectories(puth);
                var directories1 = Directory.EnumerateDirectories(puth, Word);

                var file = Directory.EnumerateFiles(puth, Word);

                if (directories1 != null)
                {
                    foreach (var item in directories1)
                    {
                        vs.Add(item);
                       
                    }
                }
                if (directories != null)
                {
                    foreach (var item in directories)
                    {
                        
                        stack.Push(item);
                    }
                }

                if (file != null)
                {
                    foreach(var item in file)
                    {
                        vs.Add(item);
                    }
                } 
            }
            return vs;
        }

        public IEnumerable Tree()
        {
            
            foreach(var item in ListDocuments())
            {
                yield return item;
            }
        }


    }
}
