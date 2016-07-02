using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FluentModelBuilder.Conventions
{
    public abstract class AbstractEntityConvention : IModelBuilderConvention
    {
        public virtual void Override(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                Override(entityType);
            }
        }

        protected abstract void Override(IMutableEntityType entityType);
    }
}
