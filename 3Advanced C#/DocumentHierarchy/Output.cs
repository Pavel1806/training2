using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHierarchy
{
    class Output
    {
        public Output(MyEvent myEvent)
        {
            myEvent.myEvent += OutputToTheConsole;
        }

        private void OutputToTheConsole(object sender, FlagsEventArgs e)
        {
            Console.WriteLine(e.MessageStart);
        }

    }
}
