using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс для получения данных из конфигурации
    /// </summary> // TODO: Можно обойтись без этого класса, подумать как.
    public static class Configuration
    {
        /// <summary>
        /// Метод для получения прослушиваемых папок
        /// </summary>
        /// <returns>DirectoryElementCollection</returns> // TODO: Комментарий не обьясняет суть возвращаемого значения
        public static DirectoryElementCollection DirectoryListenTo()
        {
            var configuration = (SimpleConfigurationSection)ConfigurationManager.GetSection("simpleSection"); // TODO: Название секции ни о чём не говорит

            return configuration.Directories;
        }
        /// <summary>
        /// Метод для получения шаблонов обработки
        /// </summary>
        /// <returns>TemplateElementCollection</returns> // TODO: Комментарий не обьясняет суть возвращаемого значения
        public static TemplateElementCollection FileProcessingTemplates()
        {
            var configuration = (SimpleConfigurationSection)ConfigurationManager.GetSection("simpleSection"); // TODO: Название секции ни о чём не говорит

            return configuration.Templates;
        }
    }
}
