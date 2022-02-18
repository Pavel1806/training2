using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс создания объекта прослушиваемой папки
    /// </summary>
    public class FolderListenElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string FolderListen
        {
            get { return (string) this["name"]; }
        }
    }
}
