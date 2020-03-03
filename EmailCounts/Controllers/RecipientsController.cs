namespace EmailCounts.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using Services;

    public class RecipientsController : Controller
    {
        private readonly NHibernateDbService _dbService;

        public RecipientsController(IOptions<AppSettings> appSettings)
        {
            _dbService = new NHibernateDbService(appSettings.Value);
        }

        public IActionResult Recipients()
        {
            List<Recipients> model = _dbService.GetAll<Recipients>();
            return View(model);
        }
    }
}
