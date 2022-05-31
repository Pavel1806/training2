using System;
using System.Collections.Generic;
using СopyingSite;

namespace SiteFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>() { ".jpeg", ".jpg", ".png", ".pdf", ".svg" };

            // ".jpeg", ".jpg", ".png", ".pdf", 

            CopySite cs = new CopySite("https://www.cosmorelax.ru/", "мебель", list);

            cs.pageHandler += Cs_pageHandler;

            cs.Copy();
        }

        private static void Cs_pageHandler(string link)
        {
            Console.WriteLine(link);
        }
    }
}
