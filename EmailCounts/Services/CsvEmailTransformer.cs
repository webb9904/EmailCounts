namespace EmailCounts.Services
{
    using System.Linq;
    using Models;

    public class CsvEmailTransformer
    {
        public DbEmail Convert(CsvEmail csvEmail, int id)
        {
            var dbEmail = new DbEmail
            {
                Id = id,
                SentDate = csvEmail.SentDateTime.Date,
                SentDateTime = csvEmail.SentDateTime,
                RecipientAddress = RecipientAddressCleaner(csvEmail.RecipientAddress),
                SenderAddress = csvEmail.SenderAddress,
                SenderDomain = SenderDomain(csvEmail.SenderAddress)
            };

            return dbEmail;
        }

        private static string RecipientAddressCleaner(string recipientAddress)
        {
            return recipientAddress.Split('#').First();
        }

        private static string SenderDomain(string senderAddress)
        {
            return senderAddress.Split('@').Last();
        }
    }
}
