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

        IEnumerable<string> ListForFoldersAndFiles { get; set; }

        Algorithm MethodForTheAlgorithm { get; set; }

       
        public FileSystemVisitor(string path, Algorithm methodForTheAlgorithm)
        {
            Path = path;
            MethodForTheAlgorithm = methodForTheAlgorithm;
            ListForFoldersAndFiles = new List<string>();
        }

        public void DelegateExecution()
        {
            string thePathToFilter = "";

            bool p = MethodForTheAlgorithm(thePathToFilter);
        }

        public void CollectingTreeOfFoldersAndFiles()
        {
            string path = ListForFoldersAndFiles;


            ListForFoldersAndFiles = Directory.GetFileSystemEntries(Path);
        }


        IEnumerable<string> Metod()
        {
            yield return ListForFoldersAndFiles;
        }

    }
}

