using ADO_NET.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T t);

        void Update(T t);
    }
}
