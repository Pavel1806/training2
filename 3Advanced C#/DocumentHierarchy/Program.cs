using System;
using System.Collections;
using System.Collections.Generic;

namespace DocumentHierarchy
{
    class Program
    {
        static void Main(string[] args)
        {
            //string puth = $"D:\\VisualStudio\\repos\\training";
            string puth = $"D:\\VisualStudio\\repos\\training\\2Introduction .net";

            FileSystemVisitor fileSystem = new FileSystemVisitor(puth, "");

            var t = fileSystem.Tree();

            foreach(var item in t)
            {
                Console.WriteLine(item);
            }

        }
    }
}
