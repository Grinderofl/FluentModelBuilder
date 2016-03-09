using FluentModelBuilder.Alterations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ModelBuilderSample
{
    public class TestEntityOverride : IEntityTypeOverride<TestEntity>
    {
        public void Override(EntityTypeBuilder<TestEntity> mapping)
        {
            mapping.Property<string>("MyProperty");
        }
    }
}