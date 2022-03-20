using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LessonXml.Tests
{
    [TestClass]
    public class ProcessingXMLTests
    {
        [TestMethod]
        public void WriteXML_Two_Objects_List()
        {
            ProcessingXML xml = new ProcessingXML();

            xml.WriteXML();

            int actual = 0;

            var col = new List<string>();

            foreach (var item in xml.ReadXmlBook())
            {
                col.Add(item.Title);
            }

            actual = col.Count;

            int expected = 2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadXmlBook_Two_Objects_List()
        {
            ProcessingXML xml = new ProcessingXML();

            xml.ReadXmlBook();

            int actual = 0;

            var col = new List<string>();

            foreach (var item in xml.ReadXmlBook())
            {
                col.Add(item.Title);
            }

            actual = col.Count;

            int expected = 2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadXmlNewspaper_Two_Objects_List()
        {
            ProcessingXML xml = new ProcessingXML();

            xml.ReadXmlNewspaper();

            int actual = 0;

            var col = new List<string>();

            foreach (var item in xml.ReadXmlNewspaper())
            {
                col.Add(item.Title);
            }

            actual = col.Count;

            int expected = 2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadXmlPatent_Two_Objects_List()
        {
            ProcessingXML xml = new ProcessingXML();

            xml.ReadXmlPatent();

            int actual = 0;

            var col = new List<string>();

            foreach (var item in xml.ReadXmlPatent())
            {
                col.Add(item.Title);
            }

            actual = col.Count;

            int expected = 2;

            Assert.AreEqual(expected, actual);
        }
    }
}
