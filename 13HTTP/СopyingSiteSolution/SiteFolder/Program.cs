using System;
using СopyingSite;

namespace SiteFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            CopySite cs = new CopySite("https://www.cosmorelax.ru/", "Сosmorelax");

            cs.pageHandler += Cs_pageHandler;

            cs.Copy();
        }

        private static void Cs_pageHandler(string link)
        {
            Console.WriteLine(link);
        }
    }
}
