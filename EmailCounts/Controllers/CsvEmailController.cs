namespace EmailCounts.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using Services;

    [Route("api/[controller]")]
    [ApiController]
    public class CsvEmailController : ControllerBase
    {
        private readonly NHibernateDbService _dbService;

        public CsvEmailController(IOptions<AppSettings> appSettings)
        {
            _dbService = new NHibernateDbService(appSettings.Value);
        }

        // GET api/csvEmail/path
        [HttpGet]
        public void Read(string path)
        {
            var reader = new CsvReaderHandler();
            Save(reader.Read(path));
        }

        private void Save(List<DbEmail> data)
        {
            _dbService.InsertDataToDb(data);
        }
    }
}
