namespace EmailCounts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Maps;
    using Models;
    using NHibernate;

    public class NHibernateDbService
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateDbService(AppSettings appSettings)
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(appSettings.ConnectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DbEmailMap>()
                .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                .ExposeConfiguration(cfg => cfg.SetProperty("adonet.batch", "1"))
                .BuildSessionFactory();
        }

        public int KeyReader()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var nextId = session.Query<DbEmail>()
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();

                return nextId == null ? 1 : nextId.Id += 1;
            }
        }

        public void InsertDataToDb(List<DbEmail> dbEmails)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.SetBatchSize(100000);
                session.FlushMode = FlushMode.Auto;

                using (var transaction = session.BeginTransaction())
                {
                    foreach (var dbEmail in dbEmails)
                    {
                        session.Save(dbEmail);
                    }

                    transaction.Commit();
                }
            }
        }

        public int GetEmailCount(DateTime date)
        {
            int count;

            using (var session = _sessionFactory.OpenSession())
            {
                count = session
                    .Query<DbEmail>()
                    .Count(x => x.SentDate == date);
            }

            return count;
        }

        public List<Recipient> GetFilteredRecipients(string filter)
        {
            List<Recipient> models;

            using (var session = _sessionFactory.OpenSession())
            {
                models = session.Query<Recipient>()
                    .Where(x => x.EmailAddress.Contains(filter) || x.Department.Contains(filter))
                    .ToList();
            }

            return models;
        }

        public List<T> GetAll<T>()
        {
            List<T> models;

            using (var session = _sessionFactory.OpenSession())
            {
                models = session.Query<T>().ToList();
            }

            return models;
        }

        public void Save<T>(T obj)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.Save(obj);
            }
        }

        public void Update<T>(T obj)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(obj);

                    transaction.Commit();
                }
            }
        }

        public void Delete<T>(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var query = session.Get<T>(id);

                session.Delete(query);

                session.Flush();
            }
        }
    }
}
