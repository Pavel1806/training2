using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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

            fileSystem.EventStartTree += OutputToTheConsole;
            fileSystem.EventFinishTree += FileSystem_EventFinishTree;
            fileSystem.EventFileFinded += FileSystem_FilteredFileFinded;
            fileSystem.EventDirectoryFinded += FileSystem_EventDirectoryFinded;
            fileSystem.EventFilteredFileFinded += FileSystem_EventFilteredFileFinded;
            fileSystem.EventFilteredDirectoryFinded += FileSystem_EventFilteredDirectoryFinded;

            //var col = fileSystem.SearchTreeOfFoldersAndFiles();

            foreach (var item in fileSystem.SearchTreeOfFoldersAndFiles())
            {
                Console.WriteLine(item);
            }

        }

        private static void FileSystem_EventFilteredDirectoryFinded(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.Message);
            e.FlagToStopSearch = true;
        }

        private static void FileSystem_EventFilteredFileFinded(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void FileSystem_EventDirectoryFinded(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void FileSystem_FilteredFileFinded(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void FileSystem_EventFinishTree(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        static void OutputToTheConsole(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
