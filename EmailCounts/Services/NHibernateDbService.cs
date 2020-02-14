namespace EmailCounts.Services
{
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

        public List<Recipients> GetRecipientsToCapture()
        {
            List<Recipients> recipients;

            using (var session = _sessionFactory.OpenSession())
            {
                recipients = session.Query<Recipients>()
                    .ToList();
            }

            return recipients;
        }
    }
}
