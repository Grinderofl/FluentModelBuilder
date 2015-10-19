using System;
using System.Collections.Generic;
using System.Linq;
using FluentModelBuilder.Contributors;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder
{
    public class FluentBuilderContributor : IFluentBuilderContributor
    {
        public virtual void Contribute(ModelBuilder modelBuilder, DbContext dbContext)
        {
            var services = dbContext.GetService<IDbContextServices>();
            var options = services.ContextOptions;
            var extension = options.FindExtension<FluentModelBuilderExtension>();
            ApplyEntities(extension.Entities, modelBuilder);
            ApplyOverrides(extension.Overrides, modelBuilder);
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