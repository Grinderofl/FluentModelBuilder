using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FluentModelBuilder.Conventions
{
    public abstract class AbstractEntityConvention : IModelBuilderConvention
    {
        public virtual void Apply(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                Apply(entityType);
            }
        }

        protected abstract void Apply(IMutableEntityType entityType);
    }
}
