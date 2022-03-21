using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectXml2.Model
{
    /// <summary>
    /// Модель газета
    /// </summary>
    public class Newspaper
    {
        /// <summary>
        /// Название газеты
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Город издания
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Издатель
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// Год публикации
        /// </summary>
        public int YearPublication { get; set; }
        /// <summary>
        /// Количество страниц
        /// </summary>
        public int NumberPages { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Количество страниц
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Дата издания
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Международный стандартный номер серийного издания 
        /// </summary>
        public string Isbn { get; set; }

        public Newspaper(string title)
        {
            Title = title;
        }
    }
}
