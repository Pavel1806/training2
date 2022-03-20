using System;
using System.Collections.Generic;
using System.Text;

namespace LessonXml
{
    //TODO: Комментарии ко всем классам и публичным свойствам
    //TODO: Сущности можно перенести в отдельную папку со своим namespace'ом, проект выглядит как каша 
    public class Newspaper
    {
       public string Title { get; set; }
        public string City { get; set; }
        public string Publisher { get; set; }
        public int YearPublication { get; set; }
        public int NumberPages { get; set; }
        public string Note { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Isbn { get; set; }

        public Newspaper(string title)
        {
            Title = title;
        }
    }
}
