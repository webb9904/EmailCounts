namespace EmailCounts.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Recipient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        public string Department { get; set; }
    }
}
