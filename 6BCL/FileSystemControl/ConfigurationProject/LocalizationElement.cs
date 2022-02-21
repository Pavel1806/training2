using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl.ConfigurationProject
{
    /// <summary>
    /// Класс создания объектов английской локализации
    /// </summary>
    public class LocalizationElement : ConfigurationElement
    {
        [ConfigurationProperty("localization", IsKey = true)]
        public string Localization
        {
            get { return (string)this["localization"]; }
        }
    }
}
