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
            List<Book> Books = new List<Book>()
            {
                new Book(){City = "Москва", Title = "Война и мир", NumberPages = 123 },
                new Book(){City = "Питер", Title = "Отцы и дети", NumberPages = 376 }
            };

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("Trial.xml", settings);

            writer.WriteStartElement("books");

            foreach (var item in Books)
            {

                writer.WriteStartElement("book");

                writer.WriteAttributeString("title",item.Title);
                //writer.WriteString(item.Title);

                writer.WriteStartElement("City");

                writer.WriteString(item.City);

                writer.WriteEndElement();

                writer.WriteStartElement("NumberPages");

                writer.WriteString(item.NumberPages.ToString());

                writer.WriteEndElement();

                writer.WriteEndElement();
               
            }

            writer.WriteEndElement();
            writer.Flush();
            writer.Close();

        }
    }
}
