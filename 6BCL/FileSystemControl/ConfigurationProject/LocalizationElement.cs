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
        [ConfigurationProperty("isAddEn")]
        public bool IsAddEn
        {
            get { return (bool) this["isAddEn"]; }
        }
    }
}
