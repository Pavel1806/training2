using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl.ConfigurationProject
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

        [ConfigurationProperty("typesOfLocalization")]
        public LocalizationElement Localization
        {
            get { return (LocalizationElement) this["typesOfLocalization"]; }
        }

        [ConfigurationProperty("fileProcessingTemplates")]
        public TemplateElementCollection FileTrackingTemplates
        {
            get { return (TemplateElementCollection)this["fileProcessingTemplates"]; }
        }
    }
}
