using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.InMemory;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder.InMemory
{
    public class InMemoryFluentModelSource : InMemoryModelSource
    {
        private readonly IFluentBuilderApplier _applier;
        public InMemoryFluentModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder, IFluentBuilderApplier applier) : base(setFinder, coreConventionSetBuilder)
        {
            _applier = applier;
        }

        protected override void FindSets(ModelBuilder modelBuilder, DbContext context)
        {
            base.FindSets(modelBuilder, context);
            _applier.Apply(modelBuilder, context);
        }
    }

    public class InMemoryProvider : IProvider
    {
        public void ApplyServices(EntityFrameworkServicesBuilder services)
        {
            services.AddInMemoryDatabase();
            services.GetService().Replace(ServiceDescriptor.Singleton<InMemoryModelSource, InMemoryFluentModelSource>());
        }
    }

    public class InMemoryModelSourceBuilder : IModelSourceBuilder
    {
        public void ApplyServices(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton(typeof (InMemoryModelSource),
                typeof (InMemoryFluentModelSource)));
        }

        public void ApplyServices(EntityFrameworkServicesBuilder builder)
        {
            builder.AddInMemoryDatabase();
        }
    }

    public static class Extensions
    {
        public static FluentModelBuilderExtension UsingInMemory(this FluentModelBuilderExtension fmb)
        {
            fmb.UseModelSource(new InMemoryModelSourceBuilder());
            return fmb;
        }
    }
}
