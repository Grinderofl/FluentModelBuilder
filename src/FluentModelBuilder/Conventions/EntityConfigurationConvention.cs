using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Conventions
{
    /// <summary>
    /// Convention for adding and configuring single entity to model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityConfigurationConvention<T> : IModelBuilderConvention where T : class
    {
        private readonly Action<EntityTypeBuilder<T>> _action;

        public EntityConfigurationConvention(Action<EntityTypeBuilder<T>> action)
        {
            _action = action;
        }

        public void Apply(ModelBuilder builder)
        {
            _action(builder.Entity<T>());
        }
    }
}
