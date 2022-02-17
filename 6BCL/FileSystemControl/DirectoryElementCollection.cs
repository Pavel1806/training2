using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс получения списка прослушиваемых папок
    /// </summary>
   public class DirectoryElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DirectoryElement)element).DirectoryName;
        }
    }
}
