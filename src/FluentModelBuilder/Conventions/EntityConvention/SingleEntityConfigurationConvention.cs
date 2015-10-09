using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Conventions.EntityConvention
{
    /// <summary>
    /// Convention for adding and configuring single entity to model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingleEntityConfigurationConvention<T> : IModelBuilderConvention where T : class
    {
        private readonly Action<EntityTypeBuilder<T>> _action;

        public SingleEntityConfigurationConvention(Action<EntityTypeBuilder<T>> action)
        {
            _action = action;
        }

        public void Apply(ModelBuilder builder)
        {
            _action(builder.Entity<T>());
        }
    }
}
