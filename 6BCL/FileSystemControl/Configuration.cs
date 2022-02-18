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
        /// Метод для получения шаблонов обработки
        /// </summary>
        /// <returns>Шаблоны обработки файлов</returns>
        public static TemplateElementCollection FileProcessingTemplates()
        {
            var configuration = (ConfigurationProjectDataSection)ConfigurationManager.GetSection("projectDataSection");

            return configuration.FileTrackingTemplates;
        }
        /// <summary>
        /// Метод получения прослушиваемой папки
        /// </summary>
        /// <returns>Прослушиваемая папка</returns>
        public static string FolderListenTo()
        {
            var configuration = (ConfigurationProjectDataSection)ConfigurationManager.GetSection("projectDataSection");
            
            return configuration.FolderListen.FolderListen;
        }
        /// <summary>
        /// Метод получения локализации
        /// </summary>
        /// <returns>если true, то возвращается русская локализация</returns>
        public static bool LocalizationIsAddRu()
        {
            var configuration = (ConfigurationProjectDataSection)ConfigurationManager.GetSection("projectDataSection");

            return configuration.Localization.IsAddRu;
        }
    }
}
