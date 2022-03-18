using ClassObjects;
using System;


namespace ProjectXML
{
    class Program
    {
        static void Main(string[] args) // TODO: В условиях задачи есть требование к написанию библиотеки и тестов к ней.
                                        // Никакого консольного приложения не нужно.
        {
            XML xml = new XML();
            //xml.WriteXML();
            xml.ReadXML();
        }
    }
}
