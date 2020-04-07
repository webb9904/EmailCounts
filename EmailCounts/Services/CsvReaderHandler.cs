namespace EmailCounts.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Maps;
    using Microsoft.Extensions.Options;
    using Models;

    public class CsvReaderHandler
    {
        private readonly NHibernateDbService _dbService;

        public CsvReaderHandler(IOptions<AppSettings> appSettings)
        {
            _dbService = new NHibernateDbService(appSettings.Value);
        }

        public List<DbEmail> Read(string path)
        {
            var dbEmail = new List<DbEmail>();

            var directory = new DirectoryInfo(path);

            var files = directory.GetFiles("*.csv");

            var transformer = new CsvEmailTransformer();

            var id = _dbService.KeyReader();

            foreach (var file in files)
            {
                dbEmail.AddRange(GetContents(file.FullName)
                    .Select(csvEmail => transformer.Convert(csvEmail, id++)));
            }

            return dbEmail;
        }

        private static IEnumerable<CsvEmail> GetContents(string path)
        {
            using (var stream = new StreamReader(path, Encoding.Unicode))
            {
                using (var csvReader = new CsvReader(stream, Configuration()))
                {
                    return csvReader.GetRecords<CsvEmail>().ToList();
                }
            }
        }

        private static Configuration Configuration()
        {
            var config = new Configuration();

            config.RegisterClassMap<CsvEmailMap>();
            config.HasHeaderRecord = true;
            config.QuoteAllFields = true;
            config.Quote = '"';
            config.BadDataFound = null;
            return config;
        }
    }
}
