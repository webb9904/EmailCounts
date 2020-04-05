namespace EmailCounts.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
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

        public IActionResult List(string selected)
        {
            var model = !string.IsNullOrEmpty(selected) ?
                _dbService.GetFilteredRecipients(selected) :
                _dbService.GetAll<Recipient>();

            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.DepartmentList = new SelectList(Departments(), "DepartmentName", "DepartmentName");

            return View();
        }

        public IActionResult Edit(int id)
        {
            var model = _dbService.GetAll<Recipient>().Where(x => x.Id == id).FirstOrDefault();

            ViewBag.DepartmentList = new SelectList(Departments(), "DepartmentName", "DepartmentName");

            return View(model);
        }

        public IActionResult Save(Recipient recipient)
        {
            if (ModelState.IsValid)
            {
                _dbService.Save(recipient);

                return Redirect("/Recipients/List");
            }

            ViewBag.DepartmentList = new SelectList(Departments(), "DepartmentName", "DepartmentName");

            return View("Create");
        }

        public IActionResult Update(Recipient recipient)
        {
            if (ModelState.IsValid)
            {
                _dbService.Update(recipient);

                return Redirect("/Recipients/List");
            }

            ViewBag.DepartmentList = new SelectList(Departments(), "DepartmentName", "DepartmentName");

            return View("Edit");
        }

        public IActionResult Delete(int id)
        {
            _dbService.Delete<Recipient>(id);

            return Redirect("/Recipients/List");
        }

        private List<Department> Departments()
        {
            return _dbService.GetAll<Department>();
        }
    }
}
