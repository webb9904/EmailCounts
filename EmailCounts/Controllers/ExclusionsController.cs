namespace EmailCounts.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using Services;

    public class ExclusionsController : Controller
    {
        private readonly NHibernateDbService _dbService;

        public ExclusionsController(IOptions<AppSettings> appSettings)
        {
            _dbService = new NHibernateDbService(appSettings.Value);
        }

        public IActionResult Exclusions()
        {
            return View();
        }
    }
}
