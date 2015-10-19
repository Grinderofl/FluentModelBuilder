using System;
using System.Collections.Generic;
using System.Linq;
using FluentModelBuilder.Contributors;
using FluentModelBuilder.Contributors.Core;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder
{
    public class ModelBuilderMutator : IModelBuilderMutator
    {
        public virtual void Apply(ModelBuilder modelBuilder, DbContext dbContext)
        {
            var services = dbContext.GetService<IDbContextServices>();
            var options = services.ContextOptions;
            var extension = options.FindExtension<FluentModelBuilderExtension>();
            ApplyAssemblies(extension.Assemblies, extension.Entities, extension.Overrides);
            ApplyEntities(extension.Entities, modelBuilder);
            ApplyOverrides(extension.Overrides, modelBuilder);
        }

        protected virtual void ApplyAssemblies(AssembliesBuilder builder, EntitiesBuilder entities, OverridesBuilder overrides)
        {
            var entityDiscoveryContributors =
                entities.Contributors.Where(x => x is DiscoveryContributorBase).Cast<DiscoveryContributorBase>();

            var overrideDiscoveryContributors =
                overrides.Contributors.Where(x => x is DiscoveryContributorBase).Cast<DiscoveryContributorBase>();

            ApplyAssemblies(builder, entityDiscoveryContributors);
            ApplyAssemblies(builder, overrideDiscoveryContributors);

        }

        private void ApplyAssemblies(AssembliesBuilder builder, IEnumerable<DiscoveryContributorBase> contributors)
        {
            foreach (var modelBuilderContributor in contributors)
                modelBuilderContributor.SetAssembliesBuilder(builder);
        }
       

        protected virtual void ApplyOverrides(OverridesBuilder builder, ModelBuilder modelBuilder)
        {
            builder.Apply(modelBuilder);
        }

        protected virtual void ApplyEntities(EntitiesBuilder builder, ModelBuilder modelBuilder)
        {
            builder.Apply(modelBuilder);
        }
    }
}