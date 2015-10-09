using System;
using System.Collections.Generic;
using System.Linq;
using FluentModelBuilder.Conventions.EntityConvention.Options;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions.OverrideConvention
{
    /// <summary>
    /// Convention for adding entity type overrides from specified assemblies
    /// </summary>
    public class OverrideDiscoveryConvention : IModelBuilderConvention
    {
        public OverrideDiscoveryConvention() : this(new EntityTypeOverrideDiscoveryConventionOptions())
        {
            
        }
        public OverrideDiscoveryConvention(EntityTypeOverrideDiscoveryConventionOptions conventionOptions)
        {
            Options = conventionOptions;
        }

        public OverrideDiscoveryConvention(Action<EntityTypeOverrideDiscoveryConventionOptions> optionsAction) : this()
        {
            optionsAction(Options);
        }
        
        /// <summary>
        /// Options for configuring <see cref="OverrideDiscoveryConvention"/>
        /// </summary>
        public EntityTypeOverrideDiscoveryConventionOptions Options { get; }

        public virtual void Apply(ModelBuilder builder)
        {
            var types = FindTypes();

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
                var entity = ModelBuilderHelper.EntityMethod.MakeGenericMethod(target)
                    .Invoke(builder, new object[] {});

                // entityTypeOverride = new IEntityTypeOverride<T>()
                var entityTypeOverride = Activator.CreateInstance(type);

                // entityTypeOverride.Configure(entity);
                method.Invoke(entityTypeOverride, new[] {entity});

            }
        }

        protected virtual IEnumerable<Type> FindTypes()
        {
            var types = Options.AssemblySources.SelectMany(x => x.GetAssemblies())
                .Distinct()
                .SelectMany(x => x.GetExportedTypes())
                .Where(x => x.ImplementsInterfaceOfType(typeof (IEntityTypeOverride<>)))
                .Distinct();
            return types;
        }
    }
}
