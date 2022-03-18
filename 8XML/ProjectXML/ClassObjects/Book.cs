using System;

namespace ClassObjects
{
    //TODO: Комментарии ко всем классам и публичным свойствам
    //TODO: Сущности можно перенести в отдельную папку со своим namespace'ом, проект выглядит как каша 
    public class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public string City { get; set; }
        public string Publisher { get; set; }
        public int YearPublication { get; set; }
        public int NumberPages { get; set; }
        public string Note { get; set; }
        public string Isbn { get; set; }
    }
}
