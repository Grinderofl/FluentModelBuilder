using Microsoft.Data.Entity.Metadata.Builders;

namespace ConventionModelBuilder.Conventions.Overrides
{
    public interface IEntityTypeOverride<T> where T : class
    {
        void Configure(EntityTypeBuilder<T> mapping);
    }
}