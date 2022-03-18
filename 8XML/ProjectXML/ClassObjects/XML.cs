using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace ClassObjects // TODO: Имя проекта "ClassObject" ни о чём не говорит. Необходимо переделать.
{
    public class XML // TODO: Ни о чём не говорящее название, отсутсвуют комментарии
                    // TODO: В классе отсутствует метод чтения
                    // TODO: В проекте нет ничего определяющего конкретный формат для XML и никаких валидаций читаемого XML соответственно.
    {
        
        public void WriteXML() // Отсутсвуют комментарии
        {
            DataSource dataSource = new DataSource();

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("Trial.xml", settings);

            writer.WriteStartElement("catalog");

            foreach (var book in dataSource.Books) 
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

            foreach (var newspaper in dataSource.Newspapers) 
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

            foreach (var patent in dataSource.Patents)
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

       public void ReadXML()
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.IgnoreComments = true;

            settings.IgnoreWhitespace = true;

            XmlReader reader = XmlReader.Create("Trial.xml", settings);

            reader.ReadToFollowing("catalog");

            while (true)
            {
                reader.ReadToDescendant("book");

                var i = reader.GetAttribute("title");

                Console.WriteLine(i);

                var t = reader.ReadToDescendant("City");

                var u = reader.ReadElementContentAsString();

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                //reader.ReadToNextSibling("Author");

                reader.ReadToDescendant("Name");

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                reader.ReadEndElement();

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                reader.ReadEndElement();

                if (reader.LocalName != "book")
                    break;
            }

            while(true)
            {
                Console.WriteLine(reader.GetAttribute("title"));

                reader.ReadToDescendant("City");

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                reader.ReadEndElement();

                if (reader.LocalName != "newspaper")
                    break;
            }

            while (true)
             {
                Console.WriteLine(reader.GetAttribute("title"));

                reader.ReadToDescendant("Country");

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                reader.ReadToDescendant("Name");

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                reader.ReadEndElement();

                Console.WriteLine(reader.ReadElementContentAsString());

                Console.WriteLine(reader.ReadElementContentAsString());

                reader.ReadEndElement();

                if (reader.LocalName != "patent")
                    break;
            }
        }
    }
}
