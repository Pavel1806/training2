using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{

    public class SimpleConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("appName")]
        public string ApplicationName
        {
            get { return (string)base["appName"]; }
        }

        [ConfigurationProperty("foldersListenTo")]
        public DirectoryElementCollection Directories
        {
            get { return (DirectoryElementCollection)this["foldersListenTo"]; }
        }

        [ConfigurationProperty("fileProcessingTemplates")]
        public TemplateElementCollection Templates
        {
            get { return (TemplateElementCollection)this["fileProcessingTemplates"]; }
        }
    }
}
