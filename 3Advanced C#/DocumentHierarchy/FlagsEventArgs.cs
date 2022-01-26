using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHierarchy
{
    class FlagsEventArgs
    {
        public string Message { get; set; }

        public FlagsEventArgs(string message)
        {
            Message = message;
        }
    }
}
