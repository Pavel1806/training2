﻿using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentHierarchy
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = $"D:\\VisualStudio\\repos\\training\\2Introduction .net";

            FileSystemVisitor fileSystem = new FileSystemVisitor(path, (string pathDirectoryOrFile) =>
            {
                //string substring = "WpfApp";
                //int indexOfSubstring = pathDirectoryOrFile.IndexOf(substring);
                //return indexOfSubstring != -1;                     // TODO: [многословность] Вся конструкция if else легко заменяется одним выражением "return indexOfSubstring != -1;"
                return true;
            }
            );

            fileSystem.EventStartTree += FileSystem_EventStartTree;
            fileSystem.EventFinishTree += FileSystem_EventFinishTree;
            fileSystem.EventFileFinded += FileSystem_FilteredFileFinded;
            fileSystem.EventDirectoryFinded += FileSystem_EventDirectoryFinded;
            fileSystem.EventFilteredFileFinded += FileSystem_EventFilteredFileFinded;
            fileSystem.EventFilteredDirectoryFinded += FileSystem_EventFilteredDirectoryFinded;

            foreach (var item in fileSystem.GetFoldersAndFiles())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(item);
                Console.WriteLine(("").PadRight(84, '-'));
                Console.ResetColor();              
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
            e.FlagToStopSearch = true;
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

        static void FileSystem_EventStartTree(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
