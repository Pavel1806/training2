using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl.ConfigurationProject
{
    /// <summary>
    /// Класс получения списка шаблонов обработки
    /// </summary>
    public class TemplateElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TemplateElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TemplateElement)element).DirectoryName;
        }
    }
}
