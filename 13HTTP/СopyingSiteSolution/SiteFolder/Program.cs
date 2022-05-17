using System;
using СopyingSite;

namespace SiteFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            CopySite cs = new CopySite("https://www.mebelion.ru/", "Mebelion");

            cs.Copy();
        }
    }
}
