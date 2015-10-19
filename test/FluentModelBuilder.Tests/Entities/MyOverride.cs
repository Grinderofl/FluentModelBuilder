using FluentModelBuilder.Contributors.Internal;
using FluentModelBuilder.Tests.Entities;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Tests
{
    public class MyOverride : IEntityTypeOverride<SingleEntity>
    {
        public void Configure(EntityTypeBuilder<SingleEntity> mapping)
        {
            mapping.Ignore(c => c.StringProperty);
        }
    }
}