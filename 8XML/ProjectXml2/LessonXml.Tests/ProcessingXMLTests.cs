using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectXml2;

namespace LessonXml.Tests
{
    // TODO: Комментарии к классу и публичным методам
    [TestClass]
    public class ProcessingXMLTests // TODO: По условиям задачи "Элементы никак не упорядочены и не сгруппированы." никак не проверяется.
                                    // Отсутствует формат XML файла.
    {
        [TestMethod]
        public void WriteXML_Two_Objects_List()
        {
            ProcessingXML xml = new ProcessingXML();

            xml.WriteXML();

            int actual = 0; // TODO: Ненужная переменная

            var col = new List<string>();

            foreach (var item in xml.ReadXmlBook())
            {
                col.Add(item.Title);
            }

            actual = col.Count;

            int expected = 2;   // TODO: Зачастую структура теста выглядит как:
                                // 1. Инициализация
                                // 2. Действия
                                // 3. Ожидания
                                // Инициализацию вынести вверх.

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadXmlBook_Two_Objects_List()
        {
            ProcessingXML xml = new ProcessingXML();

            xml.ReadXmlBook();

            int actual = 0; // TODO: Ненужная переменная

            var col = new List<string>();

            foreach (var item in xml.ReadXmlBook())
            {
                col.Add(item.Title);
            }

            actual = col.Count;

            int expected = 2; // TODO: Зачастую структура теста выглядит как:
                              // 1. Инициализация
                              // 2. Действия
                              // 3. Ожидания
                              // Инициализацию вынести вверх.

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadXmlNewspaper_Two_Objects_List()
        {
            ProcessingXML xml = new ProcessingXML();

            xml.ReadXmlNewspaper();

            int actual = 0; // TODO: Ненужная переменная

            var col = new List<string>();

            foreach (var item in xml.ReadXmlNewspaper())
            {
                col.Add(item.Title);
            }

            actual = col.Count;

            int expected = 2; // TODO: Зачастую структура теста выглядит как:
                              // 1. Инициализация
                              // 2. Действия
                              // 3. Ожидания
                              // Инициализацию вынести вверх.

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadXmlPatent_Two_Objects_List()
        {
            ProcessingXML xml = new ProcessingXML();

            xml.ReadXmlPatent();

            int actual = 0; // TODO: Ненужная переменная

            var col = new List<string>();

            foreach (var item in xml.ReadXmlPatent())
            {
                col.Add(item.Title);
            }

            actual = col.Count;

            int expected = 2; // TODO: Зачастую структура теста выглядит как:
                              // 1. Инициализация
                              // 2. Действия
                              // 3. Ожидания
                              // Инициализацию вынести вверх.

            Assert.AreEqual(expected, actual);
        }
    }
}
