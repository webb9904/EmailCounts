namespace EmailCounts.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class ExclusionMap : ClassMap<Exclusion>
    {
        public ExclusionMap()
        {
            Table("Exclusions");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Domain).Column("Domain").Nullable();
            Map(x => x.FullAddress).Column("FullAddress").Nullable();
        }
    }
}
