using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System;

namespace DocumentHierarchy.Tests
{
    [TestClass]
    public class FileSystemVisitorTests
    {
        [TestInitialize]
        public void Testinitialize()
        {
            var path = Path.Join(Environment.CurrentDirectory,"Tests"); 

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
                for (int i = 0; i < 5; i++)
                {
                    Directory.CreateDirectory(Path.Join(Environment.CurrentDirectory, $"Tests{i}"));
                    for (int j = 0; j < 5; j++)
                    {
                        File.Create(Path.Join(path, $"Test{i}", $"test{j}.txt"));
                    }
                }
            }

        }
        [TestMethod]
        public void GetFoldersAndFiles_NotNull()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) => { return true; });
            var col = new List<string>();
            foreach(var item in fileSystemVisitor.GetFoldersAndFiles())
            {
                col.Add(item);
            }

            CollectionAssert.AllItemsAreNotNull(col);
        }

        [TestMethod]
        public void GetFoldersAndFiles_QuantityDirectoryOrFiles_Contains_0()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) => {
                string substring = "0";
                int indexOfSubstring = pathDirectoryOrFile.IndexOf(substring);
                
                 return indexOfSubstring != -1;
            });

            int x = 0;
            foreach (var item in fileSystemVisitor.GetFoldersAndFiles())
            {
                x++;
            }

            int expected = 10;

            Assert.AreEqual(expected, x);
        }

        [TestMethod]
        public void GetFoldersAndFiles_QuantityDirectoryOrFiles_All()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) => {
                return true;
            });

            var col = new List<string>();
            foreach (var item in fileSystemVisitor.GetFoldersAndFiles())
            {
                col.Add(item);
            }

            int expected = 30;

            Assert.AreEqual(expected, col.Count);
        }

        [TestMethod]
        public void GetFoldersAndFiles_EventStartTree()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) =>
            {
                return true;
            });

            int actual = 0;
            fileSystemVisitor.EventStartTree += delegate (object sender, FlagsEventArgs e)
            {
                actual++;
            };
            foreach (var item in fileSystemVisitor.GetFoldersAndFiles())
            {
               
            }
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_EventDirectoryFinded()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) =>
            {
                return true;
            });

            int actual = 0;
            
            fileSystemVisitor.EventDirectoryFinded += delegate (object sender, FlagsEventArgs e)
            {
                actual++;
            };

            foreach (var item in fileSystemVisitor.GetFoldersAndFiles())
            {

            }
            int expected = 5;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_EventFileFinded()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) =>
            {
                return true;
            });

            int actual = 0;

            fileSystemVisitor.EventFileFinded += delegate (object sender, FlagsEventArgs e)
            {
                actual++;
            };

            foreach (var item in fileSystemVisitor.GetFoldersAndFiles())
            {

            }
            int expected = 25;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_EventFilteredFileFinded()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) =>
            {
                return true;
            });

            int actual = 0;

            fileSystemVisitor.EventFilteredFileFinded += delegate (object sender, FlagsEventArgs e)
            {
                actual++;
            };

            foreach (var item in fileSystemVisitor.GetFoldersAndFiles())
            {

            }
            int expected = 25;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_EventFilteredDirectoryFinded()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) =>
            {
                return true;
            });

            int actual = 0;

            fileSystemVisitor.EventFilteredDirectoryFinded += delegate (object sender, FlagsEventArgs e)
            {
                actual++;
            };

            foreach (var item in fileSystemVisitor.GetFoldersAndFiles())
            {

            }
            int expected = 5;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_EventFinishTree()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) =>
            {
                return true;
            });

            int actual = 0;

            fileSystemVisitor.EventFinishTree += delegate (object sender, FlagsEventArgs e)
            {
                actual++;
            };

            foreach (var item in fileSystemVisitor.GetFoldersAndFiles())
            {

            }
            int expected = 1;

            Assert.AreEqual(expected, actual);
        }
    }
}
