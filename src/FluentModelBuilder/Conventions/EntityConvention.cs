using System;
using System.Reflection;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions
{
    /// <summary>
    /// Convention for adding single entity to model
    /// </summary>
    public class EntityConvention : IModelBuilderConvention
    {
        protected Type EntityType;

        public EntityConvention(Type entityType)
        {
            EntityType = entityType;
        }

        public virtual void Apply(ModelBuilder builder)
        {
            builder.Entity(EntityType);
        }
    }
}