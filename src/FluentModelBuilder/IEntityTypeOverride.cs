using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Contributors.Internal
{
    public interface IEntityTypeOverride<T> where T : class
    {
        void Configure(EntityTypeBuilder<T> mapping);
    }
}