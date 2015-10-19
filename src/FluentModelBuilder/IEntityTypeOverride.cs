using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder
{
    public interface IEntityTypeOverride<T> where T : class
    {
        void Configure(EntityTypeBuilder<T> mapping);
    }
}