namespace EmailCounts.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class ExclusionsMap : ClassMap<Exclusions>
    {
        public ExclusionsMap()
        {
            Table("Exclusions");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Domain).Column("Domain").Nullable();
            Map(x => x.FullAddress).Column("FullAddress").Nullable();
        }
    }
}
