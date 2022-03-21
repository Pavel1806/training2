using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace LessonXml
{
    /// <summary>
    /// Класс обработки XML
    /// </summary>
    public class ProcessingXML 
    {
        private object lockerWrite = new object();
        private object lockerRead = new object();

        /// <summary>
        /// Метод для записи в данных в XML
        /// </summary>
        public void WriteXML() // Отсутсвуют комментарии
        {
            lock (lockerWrite)
            {
                DataSource dataSource = new DataSource();

                XmlWriterSettings settings = new XmlWriterSettings();

                settings.Indent = true;

                StreamWriter streamWriter = new StreamWriter("Trial.xml", false); // TODO: Привязка к конкретному потоку, по условиям задачи
                                                                                  // "Рекомендуется не привязываться к тому, что источником могут быть
                                                                                  // только файлы, вместо них используйте произвольные потоки"

                XmlWriter writer = XmlWriter.Create(streamWriter, settings);

                writer.WriteStartElement("catalog");

                Assembly assembly = Assembly.GetExecutingAssembly();

                var assemblyName = assembly.GetName().Name;

                writer.WriteStartAttribute("library");

                writer.WriteValue(assemblyName);

                writer.WriteEndAttribute();

                writer.WriteStartAttribute("creationTime");

                writer.WriteValue(DateTime.Now);

                writer.WriteEndAttribute();

                foreach (var book in dataSource.Books) // TODO: Сделать отдельный метод для записи сущности книги в XML
                                                       // Код трудно читаем
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

                foreach (var newspaper in dataSource.Newspapers) // TODO: Сделать отдельный метод для записи сущности газеты в XML
                                                                 // Код трудно читаем
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

                foreach (var patent in dataSource.Patents) // TODO: Сделать отдельный метод для записи сущности патента в XML
                                                            // Код трудно читаем
                {
                    writer.WriteStartElement("patent");

                    writer.WriteAttributeString("title", patent.Title);

                    writer.WriteStartElement("Country");

                    writer.WriteString(patent.Country);

                    writer.WriteEndElement();

                    writer.WriteStartElement("DateApplicationSubmission");

                    writer.WriteValue(patent.DateApplicationSubmission);

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

                streamWriter.Close();
            }
        }

        /// <summary>
        /// Метод для чтения объектов Book из XML
        /// </summary>
        /// <returns>Объект Book</returns>
        public IEnumerable<Book> ReadXmlBook() // TODO: Во всех методах чтения нет валидации формата
        {
            lock (lockerRead)
            {
                XmlReaderSettings settings = new XmlReaderSettings();

                settings.IgnoreComments = true;

                settings.IgnoreWhitespace = true;

                StreamReader streamReader = new StreamReader("Trial.xml"); // TODO: Привязка к конкретному потоку, по условиям задачи
                                                                           // "Рекомендуется не привязываться к тому, что источником могут быть
                                                                           // только файлы, вместо них используйте произвольные потоки"

                XmlReader reader = XmlReader.Create(streamReader, settings);

                reader.ReadToFollowing("catalog");

                while (true)
                {
                    Author author = new Author();

                    if (reader.LocalName != "book")
                        reader.ReadToDescendant("book");

                    string title = reader.GetAttribute("title");

                    Book book = new Book(title) { Author = author };

                    reader.ReadToDescendant("City");

                    book.City = reader.ReadElementContentAsString();

                    book.Isbn = reader.ReadElementContentAsString();

                    book.Note = reader.ReadElementContentAsString();

                    reader.ReadToDescendant("Name");

                    author.Name = reader.ReadElementContentAsString();

                    author.SerName = reader.ReadElementContentAsString();

                    reader.ReadEndElement();

                    book.Publisher = reader.ReadElementContentAsString();

                    book.NumberPages = reader.ReadElementContentAsInt();

                    reader.ReadEndElement();

                    yield return book;

                    if (reader.LocalName != "book")
                        break;
                }

                reader.Close();
                streamReader.Close();
            }          
        }
        /// <summary>
        /// Метод для чтения объектов Newspaper из XML
        /// </summary>
        /// <returns>Объект Newspaper</returns>
        public IEnumerable<Newspaper> ReadXmlNewspaper() // TODO: Во всех методах чтения нет валидации формата
        {
            lock(lockerRead)
            {
                XmlReaderSettings settings = new XmlReaderSettings();

                settings.IgnoreComments = true;

                settings.IgnoreWhitespace = true;

                StreamReader streamReader = new StreamReader("Trial.xml"); // TODO: Привязка к конкретному потоку, по условиям задачи
                                                                           // "Рекомендуется не привязываться к тому, что источником могут быть
                                                                           // только файлы, вместо них используйте произвольные потоки"

                XmlReader reader = XmlReader.Create(streamReader, settings);

                reader.ReadToFollowing("catalog");

                while (true)
                {
                    if (reader.LocalName != "newspaper")
                        reader.ReadToDescendant("newspaper");

                    string title = reader.GetAttribute("title");

                    Newspaper newspaper = new Newspaper(title);

                    reader.ReadToDescendant("City");

                    newspaper.City = reader.ReadElementContentAsString();

                    newspaper.Isbn = reader.ReadElementContentAsString();

                    newspaper.Number = reader.ReadElementContentAsInt();

                    newspaper.YearPublication = reader.ReadElementContentAsInt();

                    newspaper.NumberPages = reader.ReadElementContentAsInt();

                    reader.ReadEndElement();

                    yield return newspaper;

                    if (reader.LocalName != "newspaper")
                        break;
                }

                reader.Close();
                streamReader.Close();
            }
        }
        /// <summary>
        /// Метод для чтения объектов Patent из XML
        /// </summary>
        /// <returns>Объект Patent</returns>
        public IEnumerable<Patent> ReadXmlPatent() // TODO: Во всех методах чтения нет валидации формата
        {
            lock (lockerRead)
            {
                XmlReaderSettings settings = new XmlReaderSettings();

                settings.IgnoreComments = true;

                settings.IgnoreWhitespace = true;

                StreamReader streamReader = new StreamReader("Trial.xml"); // TODO: Привязка к конкретному потоку, по условиям задачи
                                                                           // "Рекомендуется не привязываться к тому, что источником могут быть
                                                                           // только файлы, вместо них используйте произвольные потоки"

                XmlReader reader = XmlReader.Create(streamReader, settings);

                reader.ReadToFollowing("catalog");

                while (true)
                {
                    Deviser deviser = new Deviser();

                    if (reader.LocalName != "patent")
                        reader.ReadToDescendant("patent");

                    string title = reader.GetAttribute("title");

                    Patent patent = new Patent(title) { Deviser = deviser };

                    reader.ReadToDescendant("Country");

                    patent.Country = reader.ReadElementContentAsString();

                    patent.DateApplicationSubmission = reader.ReadElementContentAsDateTime();

                    patent.RegistrationNumber = reader.ReadElementContentAsInt();

                    reader.ReadToDescendant("Name");

                    deviser.Name = reader.ReadElementContentAsString();

                    deviser.SerName = reader.ReadElementContentAsString();

                    reader.ReadEndElement();

                    patent.Note = reader.ReadElementContentAsString();

                    patent.NumberPages = reader.ReadElementContentAsInt();

                    reader.ReadEndElement();

                    yield return patent;

                    if (reader.LocalName != "patent")
                        break;
                }

                reader.Close();
                streamReader.Close();
            }
        }
    }
}

