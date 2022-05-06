using System;
using System.Collections.Generic;
using System.Text;

namespace NoSql_MongoDB.Intarfaces
{
    interface IRepository<T>
    {
        void Create(List<T> books);
        List<Book> GetAll();
        List<T> NumberOfInstancesIsMoreThanOne();
        List<T> NumberOfInstancesIsMoreThanOneSort();
        List<T> NumberOfInstancesIsMoreThanOne_IsNotMoreThree();
        int NumberOfInstancesIsMoreThanOne_Count();
        Book GetBookMaxCount();
        List<string> GetUniqueAuthor();
        List<string> GetBooksWithoutAnAuthor();
        void UpdateNumberOfCopies();
        void Delete();
        void AddFavorityGenre();
        void DeleteTheNumberOf3();
    }
}
