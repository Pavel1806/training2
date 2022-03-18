using System;
using System.Collections.Generic;
using System.Text;

namespace ClassObjects
{
    class DataSource
    {
		private List<Book> books;
		private List<Newspaper> newspapers;
		private List<Patent> patents;

		public List<Book> Books
		{
			get
			{
				if (books == null)
					createLists();

				return books;
			}
		}

		public List<Newspaper> Newspapers
		{
			get
			{
				if (newspapers == null)
					createLists();

				return newspapers;
			}
		}

		public List<Patent> Patents
		{
			get
			{
				if (patents == null)
					createLists();

				return patents;
			}
		}

		void createLists()
        {
			books = new List<Book>()
			{
				new Book()
				{City = "Москва", Title = "Война и мир", NumberPages = 123, Isbn = "978-5-699-12014-7", Note ="Осталось 2 шт.",
					Publisher="Литрес", YearPublication = 1856, Author = new Author{ Name = "Лев", SerName="Толстой"} },
			};

			newspapers = new List<Newspaper>()
			{
				new Newspaper(){ Title = "Труд", City="Москва", Isbn="978-5-699-27290-7", Date = DateTime.Now, Number = 5,
					NumberPages = 245, Note="Тираж 5000", Publisher="Московский комсомолец", YearPublication = 2020}
			};

			patents = new List<Patent>()
			{
				new Patent()
				{Title="Окно", Country = "Россия", DateApplicationSubmission = DateTime.Now, DatePublication= DateTime.Now, NumberPages=3489,
					RegistrationNumber=5, Note="Продлен до 2025 года", Deviser=new Deviser(){ Name="Александр", SerName="Александров"} }
			};
		}
	}
}
