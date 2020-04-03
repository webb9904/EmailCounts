namespace EmailCounts.Maps
{
    using CsvHelper.Configuration;
    using Models;

    public sealed class CsvEmailMap : ClassMap<CsvEmail>
    {
        public CsvEmailMap()
        {
            Map(c => c.SentDateTime).Name("origin_timestamp");
            Map(c => c.SenderAddress).Name("sender_address");
            Map(c => c.RecipientAddress).Name("recipient_status");
        }
    }
}
