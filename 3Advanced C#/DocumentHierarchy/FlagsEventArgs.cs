using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHierarchy
{
    class FlagsEventArgs
    {
        public string MessageStart { get; set; }
        //string MessageEnd { get; set; }

        public FlagsEventArgs(string messageStart)
        {
            MessageStart = messageStart;
        }
    }
}
