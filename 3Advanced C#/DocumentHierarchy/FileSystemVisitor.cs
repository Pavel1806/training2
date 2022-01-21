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

        public FileSystemVisitor(string puth)
        {
            Puth = puth;
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

                var file = Directory.EnumerateFiles(puth);

                if (directories != null)
                {
                    foreach (var item in directories)
                    {
                        vs.Add(item);
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
