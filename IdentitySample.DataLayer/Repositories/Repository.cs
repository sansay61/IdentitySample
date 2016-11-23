using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using System.Threading.Tasks;
using IdentitySample.BusinessLayer.Repositories;
using IdentitySample.BusinessLayer.Entities;
using IdentitySample.DataLayer;
using IdentitySample.DataLayer.Helpers;
using IdentitySample.BusinessLayer.Helpers;
using NHibernate;

namespace IdentitySample.DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private UnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }
        protected ISession Session { get { return _unitOfWork.Session; } }

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public T GetById(string id)
        {
            return Session.Get<T>(id);
        }

        public void Create(T entity)
        {
            Session.Save(entity);
        }

        public void Update(T entity)
        {
            Session.Update(entity);
        }

        public void Delete(string id)
        {
            Session.Delete(Session.Load<T>(id));
        }
    }
}