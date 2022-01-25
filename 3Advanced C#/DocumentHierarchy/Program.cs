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
                string substring = "WpfApp";
                int indexOfSubstring = p.IndexOf(substring);
                if (indexOfSubstring != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //return true;
            }
            );

            MyEvent myEvent = new MyEvent();
            myEvent.myEvent += MyEvent_myEvent;

            foreach (var item in fileSystem.CollectingTreeOfFoldersAndFiles())
            {
                Console.WriteLine(item);
            }

        }

        private static void MyEvent_myEvent(object sender, FlagsEventArgs e)
        {
            Console.WriteLine();
        }
    }
}
