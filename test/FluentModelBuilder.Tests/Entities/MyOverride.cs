using FluentModelBuilder.Alterations;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.Tests
{
    public class MyOverride : IEntityTypeOverride<SingleEntity>
    {
        public void Override(EntityTypeBuilder<SingleEntity> mapping)
        {
            mapping.Ignore(c => c.StringProperty);
        }
    }

    public class EntityTwoOverride : IEntityTypeOverride<EntityTwo>
    {
        public void Override(EntityTypeBuilder<EntityTwo> mapping)
        {
            mapping.Property<string>("NotProperty");
        }
    }
}