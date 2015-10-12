using System;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    public class EntityConfigurationDescriptor<T> : IDescriptor where T : class
    {
        private readonly Action<EntityTypeBuilder<T>> _mappingAction;

        public EntityConfigurationDescriptor(Action<EntityTypeBuilder<T>> mappingAction = null)
        {
            _mappingAction = mappingAction;
        }

        public void ApplyServices(IServiceCollection services)
        {
            services.AddInstance(typeof (IEntityTypeOverride<>), new GenericTypeOverride<T>(_mappingAction));
        }
    }
}