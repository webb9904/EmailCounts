namespace EmailCounts.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Recipients(string selected)
        {
            var model = !string.IsNullOrEmpty(selected) ? 
                _dbService.GetFilteredRecipients(selected) :
                _dbService.GetAll<Recipients>();

            return View(model);
        }

        public IActionResult Add()
        {
            List<Department> list = new List<Department>()
            {
                new Department(){Id = 1, Name = "Renewals"},
                new Department(){Id = 2, Name = "Direct Sales"},
                new Department(){Id = 3, Name = "Contact"},
                new Department(){Id = 4, Name = "COT/Trace"},
                new Department(){Id = 5, Name = "Enterprise Billing"},
                new Department(){Id = 6, Name = "Ledger"},
                new Department(){Id = 7, Name = "Operations Team Leaders"},
                new Department(){Id = 8, Name = "On/Off Boarding"},
                new Department(){Id = 9, Name = "N/A"}
            };

            ViewBag.DepartmentList = new SelectList(list, "Id", "Name");

            return View();
        }

        public IActionResult Delete(int id)
        {
            _dbService.Delete<Recipients>(id);

            return Redirect("/Recipients/Recipients");
        }
    }
}
