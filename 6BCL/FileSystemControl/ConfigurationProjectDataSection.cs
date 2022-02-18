﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс получения данных из конфигурации
    /// </summary>
    public class ConfigurationProjectDataSection : ConfigurationSection
    {
        [ConfigurationProperty("appName")]
        public string ApplicationName
        {
            get { return (string)base["appName"]; }
        }

        [ConfigurationProperty("folderListenTo")]
        public FolderListenElement FolderListen
        {
            get { return (FolderListenElement)this["folderListenTo"]; }
        }

        [ConfigurationProperty("localization")]
        public LocalizationElement Localization
        {
            get { return (LocalizationElement) this["localization"]; }
        }

        [ConfigurationProperty("fileProcessingTemplates")]
        public TemplateElementCollection FileTrackingTemplates
        {
            get { return (TemplateElementCollection)this["fileProcessingTemplates"]; }
        }
    }
}
