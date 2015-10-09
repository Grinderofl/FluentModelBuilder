using System;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions.Entities
{
    /// <summary>
    /// Convention for adding single entity to model
    /// </summary>
    public class SingleEntityConvention : IModelBuilderConvention
    {
        protected Type EntityType;

        public SingleEntityConvention(Type entityType)
        {
            EntityType = entityType;
        }

        public virtual void Apply(ModelBuilder builder)
        {
            builder.Entity(EntityType);
        }
    }
}