namespace EmailCounts.Models
{
    using System;

    public class vwAllEmails_Trace
    {
        public int Id { get; set; }

        public DateTime SentDate { get; set; }

        public DateTime SentDateTime { get; set; }

        public string SenderAddress { get; set; }

        public string SenderDomain { get; set; }

        public string RecipientAddress { get; set; }
    }
}
