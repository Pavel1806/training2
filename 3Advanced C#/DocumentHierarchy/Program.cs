using System;

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
                    return false; // TODO: [многословность] Вся конструкция if else легко заменяется одним выражением "return indexOfSubstring != -1;"
                }
            }
            );

            fileSystem.EventStartTree += FileSystem_EventStartTree;
            fileSystem.EventFinishTree += FileSystem_EventFinishTree;
            fileSystem.EventFileFinded += FileSystem_FilteredFileFinded;
            fileSystem.EventDirectoryFinded += FileSystem_EventDirectoryFinded;
            fileSystem.EventFilteredFileFinded += FileSystem_EventFilteredFileFinded;
            fileSystem.EventFilteredDirectoryFinded += FileSystem_EventFilteredDirectoryFinded;

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

        static void FileSystem_EventStartTree(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
