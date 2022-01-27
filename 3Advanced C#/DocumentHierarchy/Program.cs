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

            fileSystem.EventForNotifications += OutputToTheConsole;

            var col = fileSystem.CollectingTreeOfFoldersAndFiles();

            foreach (var item in col)
            {
                Console.WriteLine(item);
            }

        }
        static void OutputToTheConsole(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
