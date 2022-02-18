﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystemControl
{
   /// <summary>
   /// Класс для создания объекта оповещений для событий
   /// </summary>
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
