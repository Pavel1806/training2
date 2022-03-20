using System;

namespace LessonXml
{
   /// <summary>
   /// Модель книга
   /// </summary>
    public class Book
    {
        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// Город издания
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Издание
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Год издания
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
        /// Международный стандартный номер серийного издания 
        /// </summary>
        public string Isbn { get; set; }

        public Book (string title)
        {
            Title = title;
        }
    }

    
}
