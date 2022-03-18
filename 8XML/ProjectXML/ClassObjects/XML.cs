using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ClassObjects
{
    public class XML
    {
        
        public void WriteXML()
        {
            List<Book> books = new List<Book>()
            {
                new Book()
                {City = "Москва", Title = "Война и мир", NumberPages = 123, Isbn = "978-5-699-12014-7", Note ="Осталось 2 шт.", 
                    Publisher="Литрес", YearPublication = 1856, Author = new Author{ Name = "Лев", SerName="Толстой"} },
            };

            List<Newspaper> newspapers = new List<Newspaper>()
            {
                new Newspaper(){ Title = "Труд", City="Москва", Isbn="978-5-699-27290-7", Date = DateTime.Now, Number = 5, 
                    NumberPages = 245, Note="Тираж 5000", Publisher="Московский комсомолец", YearPublication = 2020}
            };

            List<Patent> patents = new List<Patent>() 
            { 
                new Patent() 
                {Title="Окно", Country = "Россия", DateApplicationSubmission = DateTime.Now, DatePublication= DateTime.Now, NumberPages=3489, 
                    RegistrationNumber=5, Note="Продлен до 2025 года", Deviser=new Deviser(){ Name="Александр", SerName="Александров"} } 
            };

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("Trial.xml", settings);

            writer.WriteStartElement("catalog");

            foreach (var book in books)
            {

                writer.WriteStartElement("book");

                writer.WriteAttributeString("title", book.Title);

                writer.WriteStartElement("City");

                writer.WriteString(book.City);

                writer.WriteEndElement();

                writer.WriteStartElement("Isbn");

                writer.WriteString(book.Isbn);

                writer.WriteEndElement();

                writer.WriteStartElement("Note");

                writer.WriteString(book.Note);

                writer.WriteEndElement();

                writer.WriteStartElement("Author");

                writer.WriteStartElement("Name");

                writer.WriteString(book.Author.Name);

                writer.WriteEndElement();

                writer.WriteStartElement("Sernamw");

                writer.WriteString(book.Author.SerName);

                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteStartElement("Publisher");

                writer.WriteString(book.Publisher);

                writer.WriteEndElement();

                writer.WriteStartElement("NumberPages");

                writer.WriteString(book.NumberPages.ToString());

                writer.WriteEndElement();

                writer.WriteEndElement();
               
            }

            foreach (var newspaper in newspapers)
            {

                writer.WriteStartElement("newspaper");

                writer.WriteAttributeString("title", newspaper.Title);

                writer.WriteStartElement("City");

                writer.WriteString(newspaper.City);

                writer.WriteEndElement();

                writer.WriteStartElement("Isbn");

                writer.WriteString(newspaper.Isbn);

                writer.WriteEndElement();

                writer.WriteStartElement("Number");

                writer.WriteString(newspaper.Number.ToString());

                writer.WriteEndElement();

                writer.WriteStartElement("YearPublication");

                writer.WriteString(newspaper.YearPublication.ToString());

                writer.WriteEndElement();

                writer.WriteStartElement("NumberPages");

                writer.WriteString(newspaper.NumberPages.ToString());

                writer.WriteEndElement();

                writer.WriteEndElement();

            }

            foreach (var patent in patents)
            {

                writer.WriteStartElement("patent");

                writer.WriteAttributeString("title", patent.Title);

                writer.WriteStartElement("Country");

                writer.WriteString(patent.Country);

                writer.WriteEndElement();

                writer.WriteStartElement("DateApplicationSubmission");

                writer.WriteString(patent.DateApplicationSubmission.ToString());

                writer.WriteEndElement();

                writer.WriteStartElement("RegistrationNumber");

                writer.WriteString(patent.RegistrationNumber.ToString());

                writer.WriteEndElement();

                writer.WriteStartElement("Deviser");

                writer.WriteStartElement("Name");

                writer.WriteString(patent.Deviser.Name);

                writer.WriteEndElement();

                writer.WriteStartElement("SerName");

                writer.WriteString(patent.Deviser.SerName);

                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteStartElement("Note");

                writer.WriteString(patent.Note);

                writer.WriteEndElement();

                writer.WriteStartElement("NumberPages");

                writer.WriteString(patent.NumberPages.ToString());

                writer.WriteEndElement();

                writer.WriteEndElement();

            }

            writer.WriteEndElement();
            writer.Flush();
            writer.Close();

        }
    }
}
