using System;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    public class EntityTypeOverrideDescriptor<T> : EntityTypeOverrideDescriptor
    {
        public EntityTypeOverrideDescriptor() : base(typeof(T))
        {
        }
    }

    public class EntityTypeOverrideDescriptor : IDescriptor
    {
        private readonly Type _type;

        public EntityTypeOverrideDescriptor(Type type)
        {
            if(type != typeof(IEntityTypeOverride<>))
                throw new InvalidOperationException($"Unable to cast type {type} to IEntityTypeOverride<>");
            _type = type;
        }

        public void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton(typeof (IEntityTypeOverride<>), _type);
        }
    }
}