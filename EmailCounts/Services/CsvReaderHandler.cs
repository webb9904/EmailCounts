namespace EmailCounts.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Maps;
    using Models;

    public class CsvReaderHandler
    {
        public List<DbEmail> Read(string path)
        {
            var dbEmail = new List<DbEmail>();

            var directory = new DirectoryInfo(path);

            var files = directory.GetFiles("*.csv");

            var transformer = new CsvEmailTransformer();

            foreach (var file in files)
            {
                dbEmail.AddRange(GetContents(file.FullName)
                    .Select(csvEmail => transformer.Convert(csvEmail)));
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
