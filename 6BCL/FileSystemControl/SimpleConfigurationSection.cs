using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{

    public class SimpleConfigurationSection : ConfigurationSection // TODO: Название simpleSection ни о чём не говорит
    {
        [ConfigurationProperty("appName")]
        public string ApplicationName
        {
            get { return (string)base["appName"]; }
        }

        [ConfigurationProperty("foldersListenTo")]
        public DirectoryElementCollection Directories // TODO: "Папки" может говорить о разных вещах в зависимости от контекста, нужно подумать над названием
        {
            get { return (DirectoryElementCollection)this["foldersListenTo"]; }
        }

        [ConfigurationProperty("fileProcessingTemplates")]
        public TemplateElementCollection Templates // TODO: "Шаблоны" может говорить о разных вещах в зависимости от контекста, нужно подумать над названием
        {
            get { return (TemplateElementCollection)this["fileProcessingTemplates"]; }
        }
    }
}
