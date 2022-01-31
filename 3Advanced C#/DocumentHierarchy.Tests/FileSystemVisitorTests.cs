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
            // TODO: [Design bag] Мы ещё поговорим об исключениях в следующем модуле. Тем не менее строить логику на ИСКЛЮЧЕНИЯХ не хорошо, они для ИСКЛЮЧИТЕЛЬНЫХ случаев предназначены. Почему бы не сделать проверку и при необходимости создать необходимые объекты?
            // исправил. Сделал проверку Exists
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
                for (int i = 0; i < 5; i++)
                {
                    Directory.CreateDirectory(Path.Join(Environment.CurrentDirectory, $"Tests{i}")); // TODO: [улучшение] Для конкатенации путей есть лучше решение, см. класс Path
                                                                                                       // исправил
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
                // TODO: [избыточность] Забегая вперёд (модуль LINQ), скажу что IEnumerable к списку можно привести через метод расширения ToList()
                // исправил
                col.Add(item);
            }

            CollectionAssert.AllItemsAreNotNull(col);
        }

        [TestMethod]
        public void GetFoldersAndFiles_QuantityDirectoryOrFiles_Contains_0()
        {
            var path = Path.Join(Environment.CurrentDirectory, "Tests");
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
            //List<string> vs = new List<string>();

            int actual = 0;
            fileSystemVisitor.EventStartTree += delegate (object sender, FlagsEventArgs e)
            {
                //vs.Add(e.Message);
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
