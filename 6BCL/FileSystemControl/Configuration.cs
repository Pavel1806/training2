using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileSystemControl
{
    /// <summary>
    /// Класс для получения данных из конфигурации
    /// </summary> // TODO: Можно обойтись без этого класса, подумать как.
    ///             // Можно отказаться от этого класса и тянуть напрямую значения из конфига
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
            
            return configuration.FolderListen.FolderListen; // TODO: Очень странно выглядит, желательно переименовать примерно на configuration.FolderListen.Name
        }
        /// <summary>
        /// Метод получения локализации
        /// </summary>
        /// <returns>если true, то возвращается русская локализация</returns>
        public static bool LocalizationIsAddRu() // TODO: См. замечание в Program.cs
        {
            var configuration = (ConfigurationProjectDataSection)ConfigurationManager.GetSection("projectDataSection");

            return configuration.Localization.IsAddRu;
        }
    }
}
