using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Conventions.Options;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions
{
    /// <summary>
    /// Convention for adding entities based on criterias from specified assemblies
    /// </summary>
    public class EntityDiscoveryConvention : IModelBuilderConvention
    {
        /// <summary>
        /// Options for <see cref="EntityDiscoveryConvention"/>
        /// </summary>
        public EntityDiscoveryConventionOptions Options { get; } = new EntityDiscoveryConventionOptions();
        
        public virtual void Apply(ModelBuilder builder)
        {
            var entities = FindEntities();
            foreach (var entity in entities)
                builder.Entity(entity);
        }

        protected virtual IEnumerable<Type> FindEntities()
        {
            var types = Options.Assemblies.SelectMany(x => x.GetExportedTypes());

            foreach (var criteria in Options.Criterias)
                types = types.Where(x => criteria.IsSatisfiedBy(x.GetTypeInfo()));
            
            return types;
        }
    }
}