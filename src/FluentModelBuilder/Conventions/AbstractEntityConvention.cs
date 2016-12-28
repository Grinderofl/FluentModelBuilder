using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FluentModelBuilder.Conventions
{
    public abstract class AbstractEntityConvention : IModelBuilderConvention
    {
        public virtual void Apply(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var entityTypeBuilder = modelBuilder.Entity(entityType.ClrType);
                Apply(entityTypeBuilder);
            }
        }

        protected abstract void Apply(EntityTypeBuilder entityType);
    }
}
