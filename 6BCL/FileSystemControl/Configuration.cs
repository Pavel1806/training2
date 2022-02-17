using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс для получения данных из конфигурации
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Метод для получения прослушиваемых папок
        /// </summary>
        /// <returns>DirectoryElementCollection</returns>
        public static DirectoryElementCollection DirectoryListenTo()
        {
            var configuration = (SimpleConfigurationSection)ConfigurationManager.GetSection("simpleSection");

            return configuration.Directories;
        }
        /// <summary>
        /// Метод для получения шаблонов обработки
        /// </summary>
        /// <returns>TemplateElementCollection</returns>
        public static TemplateElementCollection FileProcessingTemplates()
        {
            var configuration = (SimpleConfigurationSection)ConfigurationManager.GetSection("simpleSection");

            return configuration.Templates;
        }
    }
}
