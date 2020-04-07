namespace EmailCounts.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class DbEmailMap : ClassMap<DbEmail>
    {
        public DbEmailMap()
        {
            Table("AllEmails");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.SentDate).Column("SentDate").Nullable();
            Map(x => x.SentDateTime).Column("SentDateTime").Nullable();
            Map(x => x.SenderAddress).Column("SenderAddress").Nullable();
            Map(x => x.SenderDomain).Column("SenderDomain").Nullable();
            Map(x => x.RecipientAddress).Column("RecipientAddress").Nullable();
        }
    }
}
