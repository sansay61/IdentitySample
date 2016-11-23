using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using IdentitySample.BusinessLayer.Helpers;
using IdentitySample.BusinessLayer.Repositories;
using IdentitySample.DataLayer.Repositories;
using NHibernate;
using NHibernate.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IdentitySample.DataLayer.Helpers
{
    public class UnitOfWork:IUnitOfWork
    {
        private static readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;
        public ISession Session { get; private set; }

        static UnitOfWork()
        {
            if (_sessionFactory == null)
            {
                try { 
                var dbConfig = OracleDataClientConfiguration.Oracle10
                  .ConnectionString(c => c.FromConnectionStringWithKey("Oracle"))
                  .Driver<OracleDataClientDriver>()
                  .ShowSql();

                _sessionFactory = Fluently.Configure()
                  .Database(dbConfig)
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<IdentitySample.DataLayer.Mapping.ClaimMap>())
                  .BuildSessionFactory();
                    }
                catch (Exception e)
                {
                    e.ToString();
                }
            }
        }

        public UnitOfWork()
        {
            Session = _sessionFactory.OpenSession();
        }

        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction();
        }
        public void Commit()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
            finally
            {
                Session.Dispose();
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
            }
            finally
            {
                Session.Dispose();
            }
        }


    }
}
