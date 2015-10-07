using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Conventions.Overrides;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions
{
    public class EntityTypeOverrideConvention<T> : IModelBuilderConvention
    {
        public EntityTypeOverrideConvention()
        {
            if(!EntityTypeOverrideDiscoveryConvention.ImplementsInterfaceOfType(typeof(T), typeof(IEntityTypeOverride<>)))
                throw new InvalidOperationException($"{nameof(T)} is not of type IEntityTypeOverride<>");
        }

        public virtual void Apply(ModelBuilder builder)
        {
            var method = typeof (T).GetMethod("Configure");

            var target = typeof (T).GetInterfaces()
                .Single(x => x.GetGenericTypeDefinition() == typeof(IEntityTypeOverride<>))
                .GenericTypeArguments.First();

            var entity = EntityTypeOverrideDiscoveryConvention.EntityMethod.MakeGenericMethod(target)
                .Invoke(builder, new object[] {});

            var @override = Activator.CreateInstance<T>();
            method.Invoke(@override, new[] {entity});
        }
    }
}