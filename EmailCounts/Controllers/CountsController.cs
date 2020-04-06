namespace EmailCounts.Controllers
{
    using EmailCounts.Models;
    using EmailCounts.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System;

    public class CountsController : Controller
    {
        private readonly NHibernateDbService _dbService;

        public CountsController(IOptions<AppSettings> appSettings)
        {
            _dbService = new NHibernateDbService(appSettings.Value);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index(DateTime SentDate)
        {
            var model = _dbService.GetEmailCount(SentDate);

            return View(model);
        }
    }
}
