using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentHierarchy
{
    class MyDelegate
    {
        delegate void ProgressDelegate(string message, int progressPercentage);

        void WriteProgressToFile(string message, int progressPercentage)
        {
            File.ReadAllText("D:\\epam\\read.txt");
        }

        void WriteProgressToConsole(string message, int progressPercentage)
        {
            Console.WriteLine("12344345");
        }

       void ProgressEmulator(ProgressDelegate progress)
        {
            int stepCount = (int)(new Random(new TimeSpan().Seconds).NextDouble() * 10);
            
            progress("Start", 0);
            for (int step = 0; step < stepCount; step++)
            {
                progress("Progress", (int)(step * 100 / stepCount));
                System.Threading.Thread.Sleep(100);
                Console.WriteLine(step);
            }
            progress("Finish", 100);

        }

        public void Test()
        {
            ProgressDelegate p = null;
            p += WriteProgressToConsole;
            p += WriteProgressToFile;

            ProgressEmulator(p);
        }


     
    }
}
