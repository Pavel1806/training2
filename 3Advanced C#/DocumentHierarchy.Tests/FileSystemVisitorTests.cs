using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocumentHierarchy;
using System.Collections.Generic;

namespace DocumentHierarchy.Tests
{
    [TestClass]
    public class FileSystemVisitorTests
    {
        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_NotNull()
        {
            string path = $"D:\\";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string p)=> { return true; });
            var col = new List<string>();
            foreach(var item in fileSystemVisitor.CollectingTreeOfFoldersAndFiles())
            {
                col.Add(item);
            }

            CollectionAssert.AllItemsAreNotNull(col);
        }
    }
}
