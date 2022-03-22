using ProjectXml2.Model;
using System;
using System.Collections.Generic;

namespace ProjectXml2
{
    public class DataSource
    {
		private List<Book> books;
		private List<Newspaper> newspapers;
		private List<Patent> patents;

		/// <summary>
		/// Коллекция книг
		/// </summary>
		public List<Book> Books
		{
			get
			{
				if (books == null)
					createLists();

				return books;
			}
		}
		/// <summary>
		/// Коллекция газет
		/// </summary>
		public List<Newspaper> Newspapers
		{
			get
			{
				if (newspapers == null)
					createLists();

				return newspapers;
			}
		}
		/// <summary>
		/// Коллекция патентов
		/// </summary>
		public List<Patent> Patents
		{
			get
			{
				if (patents == null)
					createLists();

				return patents;
			}
		}

		void createLists() // TODO: Нарушен стиль написания имени метода.
						   // TODO: Предлагаю разбить этот метод на 3 отдельных метода
						   // Для каждого типа сущности отдельно
		{
			books = new List<Book>()
			{
				new Book("Война и мир")
				{City = "Москва", NumberPages = 123, Isbn = "978-5-699-12014-7", Note ="Осталось 2 шт.",
					Publisher="Литрес", YearPublication = 1856, Author = new Author{ Name = "Лев", SerName="Толстой"} },
				new Book("Война и мир")
				{City = "Москва", NumberPages = 123, Isbn = "978-5-699-12014-7", Note ="Осталось 2 шт.",
					Publisher="Литрес", YearPublication = 1856, Author = new Author{ Name = "Лев", SerName="Толстой"} },
			};

			newspapers = new List<Newspaper>()
			{
				new Newspaper("Труд"){ City="Москва", Isbn="978-5-699-27290-7", Date = DateTime.Now, Number = 5,
					NumberPages = 245, Note="Тираж 5000", Publisher="Московский комсомолец", YearPublication = 2020},
				new Newspaper("Труд"){ City="Москва", Isbn="978-5-699-27290-7", Date = DateTime.Now, Number = 5,
					NumberPages = 245, Note="Тираж 5000", Publisher="Московский комсомолец", YearPublication = 2020},
			};

			patents = new List<Patent>()
			{
				new Patent("Окно")
				{ Country = "Россия", DateApplicationSubmission = DateTime.Now, DatePublication= DateTime.Now, NumberPages=3489,
					RegistrationNumber=5, Note="Продлен до 2025 года", Deviser=new Deviser(){ Name="Александр", SerName="Александров"} },
				new Patent("Окно")
				{ Country = "Россия", DateApplicationSubmission = DateTime.Now, DatePublication= DateTime.Now, NumberPages=3489,
					RegistrationNumber=5, Note="Продлен до 2025 года", Deviser=new Deviser(){ Name="Александр", SerName="Александров"} },
			};
		}
	}
}
