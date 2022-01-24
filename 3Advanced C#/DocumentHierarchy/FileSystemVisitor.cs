using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentHierarchy
{
    public delegate IEnumerable<string> Algorithm(IEnumerable<string> vs);
    class FileSystemVisitor
    {
        string Path { get; set; }

        IEnumerable<string> ListForFoldersAndFiles { get; set; }

        public Algorithm MethodForTheAlgorithm { get; set; }

       
        public FileSystemVisitor(string path)
        {
            Path = path;
            ListForFoldersAndFiles = new List<string>();
        }

        public IEnumerable<string> DelegateExecution()
        {
           return MethodForTheAlgorithm(ListForFoldersAndFiles);
        }

    }
}

