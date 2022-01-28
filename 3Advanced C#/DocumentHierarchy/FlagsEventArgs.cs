using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHierarchy
{
    public class FlagsEventArgs
    {
        public string Message { get; set; }
        public bool FlagToStopSearch { get; set; }

        public FlagsEventArgs(string message)
        {
            Message = message;
        }
    }
}
