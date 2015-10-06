using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConventionModelBuilder.Conventions.Options;
using ConventionModelBuilder.Conventions.Overrides;
using Microsoft.Data.Entity;

namespace ConventionModelBuilder.Conventions
{
    /// <summary>
    /// Convention for adding entity type overrides from specified assemblies
    /// </summary>
    public class EntityTypeOverrideDiscoveryConvention : IModelBuilderConvention
    {
        /// <summary>
        /// Options for configuring <see cref="EntityTypeOverrideDiscoveryConvention"/>
        /// </summary>
        public EntityTypeOverrideDiscoveryConventionOptions Options { get; } = new EntityTypeOverrideDiscoveryConventionOptions();

        public virtual void Apply(ModelBuilder builder)
        {
            var types = FindEntities();

            // MethodCall => Entity<>()
            var entityMethod = typeof (ModelBuilder).GetMethods().First(x => x.Name == "Entity" && x.IsGenericMethod);
            foreach (var type in types)
            {
                // IEntityTypeOverride<>().Configure(ModelBuilder)
                var method = type.GetMethod("Configure");

                // <T>
                var target =
                    type.GetInterfaces()
                        .Single(x => x.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>))
                        .GenericTypeArguments.First();

                // invokedEntity = ModelBuilder.Entity<T>()
                var entity = entityMethod.MakeGenericMethod(target)
                    .Invoke(builder, new object[] {});

                // entityTypeOverride = new IEntityTypeOverride<T>()
                var entityTypeOverride = Activator.CreateInstance(type);

                // entityTypeOverride.Configure(entity);
                method.Invoke(entityTypeOverride, new[] {entity});

            }
        }

        protected virtual IEnumerable<Type> FindEntities()
        {
            var types = Options.Assemblies.SelectMany(x => x.GetExportedTypes())
                .Where(x => ImplementsInterfaceOfType(x, typeof (IEntityTypeOverride<>)));
                //.Where(x => x.GetInterfaces()
                //        .Any(
                //            c =>
                //                c.GetTypeInfo().IsGenericType &&
                //                c.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>)));
            return types;
        }

        protected virtual bool ImplementsInterfaceOfType(Type type, Type interfaceType)
        {
            var interfaces = type.GetInterfaces();
            return interfaces.Any(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
        }
    }
}
