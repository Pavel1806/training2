using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocumentHierarchy;
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
            var path = Environment.CurrentDirectory + "\\Tests";

            try
            {
                var directories = Directory.GetFileSystemEntries(path);
            }
            catch
            {
                Directory.CreateDirectory(path);
                for (int i = 0; i < 5; i++)
                {
                    Directory.CreateDirectory(path + $"\\Test{i}");
                    for (int j = 0; j < 5; j++)
                    {
                        File.Create(path + $"\\Test{i}" + $"\\test{j}.txt");
                    }
                }
            }

        }
        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_NotNull()
        {
            string path = Environment.CurrentDirectory + "\\Tests";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string p)=> { return true; });
            var col = new List<string>();
            foreach(var item in fileSystemVisitor.SearchTreeOfFoldersAndFiles())
            {
                col.Add(item);
            }

            CollectionAssert.AllItemsAreNotNull(col);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_QuantityDirectoryOrFiles_Ñontains_0()
        {
            string path = Environment.CurrentDirectory + "\\Tests";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string p) => {
                string substring = "0";
                int indexOfSubstring = p.IndexOf(substring);
                if (indexOfSubstring != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
            var col = new List<string>();
            foreach (var item in fileSystemVisitor.SearchTreeOfFoldersAndFiles())
            {
                col.Add(item);
            }

            int expected = 10;

            Assert.AreEqual(expected, col.Count);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_QuantityDirectoryOrFiles_All()
        {
            string path = Environment.CurrentDirectory + "\\Tests";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string p) => {
                return true;
            });
            var col = new List<string>();
            foreach (var item in fileSystemVisitor.SearchTreeOfFoldersAndFiles())
            {
                col.Add(item);
            }

            int expected = 30;

            Assert.AreEqual(expected, col.Count);
        }

    }
}
