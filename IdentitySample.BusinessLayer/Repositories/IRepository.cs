using IdentitySample.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.BusinessLayer.Repositories
{
    public interface IRepository<T> where T:class
    {
        IQueryable<T> GetAll();
        T GetById(string id);
        void Create(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
