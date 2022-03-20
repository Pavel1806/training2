using LessonXml;
using System;


namespace ProjectXML
{
    class Program
    {
        static void Main(string[] args) // TODO: В условиях задачи есть требование к написанию библиотеки и тестов к ней.
                                        // Никакого консольного приложения не нужно.   // Я понял. Уберу как закончу работу над уроком. Так проверять проще.
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
