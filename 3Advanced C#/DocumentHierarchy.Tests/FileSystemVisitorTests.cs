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
            var path = Path.Join(Environment.CurrentDirectory,"\\Tests"); 
            // TODO: [Design bag] Мы ещё поговорим об исключениях в следующем модуле. Тем не менее строить логику на ИСКЛЮЧЕНИЯХ не хорошо, они для ИСКЛЮЧИТЕЛЬНЫХ случаев предназначены. Почему бы не сделать проверку и при необходимости создать необходимые объекты?
            // исправил. Сделал проверку Exists
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
                for (int i = 0; i < 5; i++)
                {
                    Directory.CreateDirectory(Path.Join(Environment.CurrentDirectory, $"\\Tests{i}")); // TODO: [улучшение] Для конкатенации путей есть лучше решение, см. класс Path
                                                                                                       // исправил
                    for (int j = 0; j < 5; j++)
                    {
                        File.Create(Path.Join(path, $"\\Test{i}", $"\\test{j}.txt"));
                    }
                }
            }

        }
        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_NotNull()
        {
            var path = Path.Join(Environment.CurrentDirectory, "\\Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) => { return true; });
            var col = new List<string>();
            int x = 0;
            foreach(var item in fileSystemVisitor.GetFoldersAndFiles())
            {
                // TODO: [избыточность] Забегая вперёд (модуль LINQ), скажу что IEnumerable к списку можно привести через метод расширения ToList()
                // исправил
                x++;
            }

            CollectionAssert.AllItemsAreNotNull(col);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_QuantityDirectoryOrFiles_Contains_0()
        {
            var path = Path.Join(Environment.CurrentDirectory, "\\Tests");
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) => {
                string substring = "0"; // TODO: [читабельность] Это не ошибка, но если вместо переменной использовать литерал, код будет читаться легче. Сжатие коде не всегда благо, но здесь оно оправдано.
                int indexOfSubstring = pathDirectoryOrFile.IndexOf(substring);
                
                 return indexOfSubstring != -1; // TODO: [многословность] Вся конструкция if else легко заменяется одним выражением "return indexOfSubstring != -1;"
                                                // исправил
            });

            int x = 0;
            foreach (var item in fileSystemVisitor.GetFoldersAndFiles())
            {
                // TODO: [избыточность] Чтобы посчитать кол-во элементов, это не самая оптимальная стратегия. Нам нужен int, а мы для этого целую коллекцию породили.
                // исправил
                x++;
            }

            int expected = 10;

            Assert.AreEqual(expected, x);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_QuantityDirectoryOrFiles_All()
        {
            var path = Path.Join(Environment.CurrentDirectory, "\\Tests");
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

        //[TestMethod]
        //public void CollectingTreeOfFoldersAndFiles_()
        //{
        //    var path = Path.Join(Environment.CurrentDirectory, "\\Tests");
        //    FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string pathDirectoryOrFile) => {
        //        return true;
        //    });

        //    fileSystemVisitor.EventStartTree += FileSystemVisitor_EventStartTree;

        //    string expected = "Начали обход дерева";

        //    Assert.AreEqual(expected, );
        //}

        //private void FileSystemVisitor_EventStartTree(object sender, FlagsEventArgs e)
        //{
        //    Console.WriteLine(e.Message);
        //}
    }
}
