using ProjectXml2;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessingXML xml = new ProcessingXML();
            xml.WriteXML();

            foreach (var item in xml.ReadXmlBook())
            {
                Console.WriteLine(item.Title);
            }

            foreach (var item in xml.ReadXmlNewspaper())
            {
                Console.WriteLine(item.Title);
            }

            foreach (var item in xml.ReadXmlPatent())
            {
                Console.WriteLine(item.Title);
            }
        }
    }
}
