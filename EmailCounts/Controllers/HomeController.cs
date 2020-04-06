namespace EmailCounts.Controllers
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using Services;

    public class HomeController : Controller
    {
        private readonly NHibernateDbService _dbService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IOptions<AppSettings> appSettings, IHostingEnvironment hostingEnvironment)
        {
            _dbService = new NHibernateDbService(appSettings.Value);
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CsvFileUpload upload)
        {
            if (ModelState.IsValid)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");

                var reader = new CsvReaderHandler();

                if (upload.Files != null && upload.Files.Count > 0)
                {
                    UploadFiles(upload, uploadsFolder);

                    _dbService.InsertDataToDb(reader.Read(uploadsFolder));

                    ClearUploadsFolder(uploadsFolder);
                }
            }

            return View();
        }

        private static void UploadFiles(CsvFileUpload upload, string uploadsFolder)
        {
            foreach (IFormFile file in upload.Files)
            {
                var uniqueFileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
        }

        private static void ClearUploadsFolder(string uploadsFolder)
        {
            foreach (var file in new DirectoryInfo(uploadsFolder).GetFiles())
            {
                file.Delete();
            }
        }
    }
}
