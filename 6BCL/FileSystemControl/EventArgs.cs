using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystemControl
{
    // TODO: Комментарии ко всем публичным методам
    public class EventArgs
    {
        public DateTime TimeCreate { get; set; }
        public FileSystemEventArgs eventArgs { get; set; }
        public EventArgs(FileSystemEventArgs ev)
        {
            eventArgs = ev;
        }
        
    }
}
