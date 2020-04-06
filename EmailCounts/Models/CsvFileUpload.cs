namespace EmailCounts.Models
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class CsvFileUpload
    {
        public List<IFormFile> Files { get; set; }
    }
}
