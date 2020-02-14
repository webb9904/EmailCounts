namespace EmailCounts.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class RecipientsMap : ClassMap<Recipients>
    {
        public RecipientsMap()
        {
            Table("RecipientsToCapture");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.EmailAddress).Column("EmailAddress").Nullable();
            Map(x => x.Department).Column("Department").Nullable();
        }
    }
}
