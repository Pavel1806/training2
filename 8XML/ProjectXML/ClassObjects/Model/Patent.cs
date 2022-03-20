using System;
using System.Collections.Generic;
using System.Text;

namespace LessonXml
{
   /// <summary>
   /// Модель патент
   /// </summary>
    public class Patent
    {
        /// <summary>
        /// Название патента
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Изобретатель
        /// </summary>
        public Deviser Deviser { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Номер регистрации
        /// </summary>
        public int RegistrationNumber { get; set; }
        /// <summary>
        /// Дата подачи заявки
        /// </summary>
        public DateTime DateApplicationSubmission { get; set; }
        /// <summary>
        /// Дата публикации
        /// </summary>
        public DateTime DatePublication { get; set; }
        /// <summary>
        /// Количество страниц
        /// </summary>
        public int NumberPages { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }

        public Patent(string title)
        {
            Title = title;
        }
    }
}
