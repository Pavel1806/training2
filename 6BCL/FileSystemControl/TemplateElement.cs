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

        [ConfigurationProperty("addingDateOrNumberTheOutputFile")]
        public bool DateOrNumberTrue
        {
            get { return (bool)base["addingDateOrNumberTheOutputFile"]; }
        }

        [ConfigurationProperty("size")]
        public int FileSize // TODO: Нигде не используется
        {
            get { return (int)base["size"]; }
        }
    }
}
