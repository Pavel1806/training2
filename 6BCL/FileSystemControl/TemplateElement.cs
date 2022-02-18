using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс создания объекта шаблонов обработки
    /// </summary>
    public class TemplateElement : ConfigurationElement
    {
        [ConfigurationProperty("directory", IsKey = true)]
        public string DirectoryName
        {
            get { return (string)base["directory"]; }
        }

        [ConfigurationProperty("templateFile")]
        public string Filter
        {
            get { return (string)base["templateFile"]; }
        }

        [ConfigurationProperty("isAddDate")]
        public bool IsAddDate
        {
            get { return (bool)base["isAddDate"]; }
        }

        [ConfigurationProperty("isAddId")]
        public bool IsAddId
        {
            get { return (bool)base["isAddId"]; }
        }
    }
}
