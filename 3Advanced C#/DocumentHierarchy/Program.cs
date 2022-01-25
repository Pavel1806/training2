using System;
using System.Collections;
using System.Collections.Generic;

namespace DocumentHierarchy
{
    class Program
    {
        static void Main(string[] args)
        {           
            string path = $"D:\\VisualStudio\\repos\\training\\2Introduction .net";

            FileSystemVisitor fileSystem = new FileSystemVisitor(path, (string p) =>
            {
                string substring = "Introduction";
                int indexOfSubstring = p.IndexOf(substring);
                if(indexOfSubstring != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }              
            }
            );

            fileSystem.CollectingTreeOfFoldersAndFiles();

            //foreach (var item in filteredList)
            //{
            //    Console.WriteLine(item);
            //}

        }
    }
}
