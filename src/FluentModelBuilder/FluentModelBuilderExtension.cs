using System;
using System.Collections.Generic;
using System.Linq;
using FluentModelBuilder.Options;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder
{
    public class FluentModelBuilderExtension : IDbContextOptionsExtension
    {


        public FluentModelBuilderExtension(DbContextOptionsBuilder builder, FluentModelBuilderOptions options)
        {
            var internalBuilder = new FluentModelBuilder(options);
            ((IDbContextOptionsBuilderInfrastructure) builder).AddOrUpdateExtension(this);
            //applier.UseModel(internalBuilder.Build());
        }

        public void ApplyServices(EntityFrameworkServicesBuilder builder)
        {
            var services = builder.GetService();
            services.Replace(ServiceDescriptor.Scoped<IModelSource, FluentModelSource>());
        }
    }

    public class FluentModelSource : ModelSource
    {
        private IModelBuilderApplier _applier;

        public FluentModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder, IModelBuilderApplier applier) : base(setFinder, coreConventionSetBuilder)
        {
            _applier = applier;
        }

        protected override void FindSets(ModelBuilder modelBuilder, DbContext context)
        {
            base.FindSets(modelBuilder, context);
            _applier.Apply(modelBuilder);
        }
    }

    public interface IModelBuilderApplier
    {
        void Apply(ModelBuilder builder);
    }

    public class ModelBuilderApplier : IModelBuilderApplier
    {
        private readonly IEnumerable<IModelBuilderConvention> _conventions;

        public ModelBuilderApplier(IEnumerable<IModelBuilderConvention> conventions)
        {
            _conventions = conventions;
        }

        public void Apply(ModelBuilder builder)
        {
            foreach(var convention in _conventions)
                convention.Apply(builder);
        }
    }

    public class EntityAddingConvention : IModelBuilderConvention
    {
        private readonly IEnumerable<ITypeSource> _typeSources;

        public EntityAddingConvention(IEnumerable<ITypeSource> typeSources)
        {
            _typeSources = typeSources;
        }

        public void Apply(ModelBuilder builder)
        {
            foreach (var type in _typeSources.SelectMany(x => x.GetTypes()))
                builder.Entity(type);
        }
    }



    public interface ITypeSource
    {
        IEnumerable<Type> GetTypes();
    }
}