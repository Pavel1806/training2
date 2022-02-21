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
        [ConfigurationProperty("enUS", IsKey = true)]
        public string en_US
        {
            get { return (string)this["enUS"]; }
        }
        [ConfigurationProperty("ruRU")]
        public string ru_RU
        {
            get { return (string)this["ruRU"]; }
        }
    }
}
