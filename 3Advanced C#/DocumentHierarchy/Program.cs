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

            FileSystemVisitor fileSystem = new FileSystemVisitor(path)
            {
                MethodForTheAlgorithm = (IEnumerable<string> vs) => 
                   {
                       List<string> vs1 = new List<string>();

                       foreach (var item in vs)
                       {
                           
                       }
                       return vs1;
                   }
            };

            var filteredList = fileSystem.DelegateExecution();

            foreach (var item in filteredList)
            {
                Console.WriteLine(item);
            }

        }
    }
}
