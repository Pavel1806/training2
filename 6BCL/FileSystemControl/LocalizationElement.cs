using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс создания объектов русской локализации
    /// </summary>
    public class LocalizationElement : ConfigurationElement
    {
        [ConfigurationProperty("isAddRu")]
        public bool IsAddRu
        {
            get { return (bool) this["isAddRu"]; }
        }
    }
}
