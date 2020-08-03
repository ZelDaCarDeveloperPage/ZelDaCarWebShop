using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoShop.Models.Repository.Interface
{
    interface IRepository<T> where T : class
    {
        T Create(T item);
        T Delete(int id);
        T Update(T item);
        T Get(int id);
        IEnumerable<T> GetList();
    }
}
