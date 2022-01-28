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
            var path = Environment.CurrentDirectory + "\\Tests";

            try
            {
                var directories = Directory.GetFileSystemEntries(path);
            }
            catch // TODO: [Design bag] Мы ещё поговорим об исключениях в следующем модуле. Тем не менее строить логику на ИСКЛЮЧЕНИЯХ не хорошо, они для ИСКЛЮЧИТЕЛЬНЫХ случаев предназначены. Почему бы не сделать проверку и при необходимости создать необходимые объекты?
            {
                Directory.CreateDirectory(path);
                for (int i = 0; i < 5; i++)
                {
                    Directory.CreateDirectory(path + $"\\Test{i}"); // TODO: [улучшение] Для конкатенации путей есть лучше решение, см. класс Path
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
                col.Add(item); // TODO: [избыточность] Забегая вперёд (модуль LINQ), скажу что IEnumerable к списку можно привести через метод расширения ToList()
            }

            CollectionAssert.AllItemsAreNotNull(col);
        }

        [TestMethod]
        public void CollectingTreeOfFoldersAndFiles_QuantityDirectoryOrFiles_Contains_0()
        {
            string path = Environment.CurrentDirectory + "\\Tests";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, (string p) => {
                string substring = "0"; // TODO: [читабельность] Это не ошибка, но если вместо переменной использовать литерал, код будет читаться легче. Сжатие коде не всегда благо, но здесь оно оправдано.
                int indexOfSubstring = p.IndexOf(substring);
                if (indexOfSubstring != -1)
                {
                    return true; // TODO: [многословность] Вся конструкция if else легко заменяется одним выражением "return indexOfSubstring != -1;"
                }
                else
                {
                    return false;
                }
            });

            var col = new List<string>();
            foreach (var item in fileSystemVisitor.SearchTreeOfFoldersAndFiles())
            {
                col.Add(item); // TODO: [избыточность] Чтобы посчитать кол-во элементов, это не самая оптимальная стратегия. Нам нужен int, а мы для этого целую коллекцию породили.
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
