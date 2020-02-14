using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmailCounts.Models;

namespace EmailCounts.Controllers
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Options;
    using Services;

    public class HomeController : Controller
    {
        private readonly NHibernateDbService _dbService;

        public HomeController(IOptions<AppSettings> appSettings)
        {
            _dbService = new NHibernateDbService(appSettings.Value);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Recipients()
        {
            List<Recipients> model = _dbService.GetRecipientsToCapture();
            return View(model);
        }

        public IActionResult Exclusions()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
