using Intarfaces;
using System;

namespace ReflectionProject
{
    class Program
    {
        static void Main(string[] args)
        {
            DescriptionService dc = new DescriptionService();

            dc.MethodIntarface();
        }
    }
}
