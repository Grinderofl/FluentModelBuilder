using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Conventions.EntityConvention.Options;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions.EntityConvention
{
    /// <summary>
    /// Convention for adding entities based on criterias from specified assemblies
    /// </summary>
    public class EntityDiscoveryConvention : IModelBuilderConvention
    {
        public EntityDiscoveryConvention() : this(new EntityDiscoveryConventionOptions())
        {
        }

        public EntityDiscoveryConvention(EntityDiscoveryConventionOptions options)
        {
            Options = options;
        }

        public EntityDiscoveryConvention(Action<EntityDiscoveryConventionOptions> optionsAction) : this()
        {
            optionsAction(Options);
        }

        /// <summary>
        /// Options for <see cref="EntityDiscoveryConvention"/>
        /// </summary>
        public EntityDiscoveryConventionOptions Options { get; }
        
        public virtual void Apply(ModelBuilder builder)
        {
            var entities = FindEntities();
            foreach (var entity in entities)
                builder.Entity(entity);
        }

        protected virtual IEnumerable<Type> FindEntities()
        {
            var types =
                Options.AssemblySources.SelectMany(x => x.GetAssemblies())
                    .Distinct()
                    .SelectMany(x => x.GetExportedTypes());

            foreach (var criteria in Options.Criterias)
                types = types.Where(x => criteria.IsSatisfiedBy(x.GetTypeInfo()));
            
            return types;
        }
    }

    
}