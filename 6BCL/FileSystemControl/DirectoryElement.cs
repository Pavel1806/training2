using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс создания объекта прослушиваемых папок
    /// </summary>
    public class DirectoryElement : ConfigurationElement
    {
        /// <summary>
        /// Название папки
        /// </summary>
        [ConfigurationProperty("name", IsKey = true)]
        public string DirectoryName
        {
            get { return (string)base["name"]; }
        }

        [ConfigurationProperty("size")]
        public int FileSize
        {
            get { return (int)base["size"]; }
        }
    }
}
