using System;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder.v2
{
    public static class FluentModelBuilderExtensions
    {
        public static EntityCoreDescriptor Entities(this AbstractCoreDescriptor fmb)
        {
            return fmb.WithCoreDescriptor<EntityCoreDescriptor>();
        }

        public static AssemblyCoreDescriptor Assemblies(this AbstractCoreDescriptor fmb)
        {
            return fmb.WithCoreDescriptor<AssemblyCoreDescriptor>();
        }

        public static FluentModelBuilder AddEntity<T>(this FluentModelBuilder fmb)
        {
            fmb.Entities.Add<T>();
            return fmb;
        }

        public static FluentModelBuilder AddAssemblyContaining<T>(this FluentModelBuilder fmb)
        {
            //fmb.Assemblies.AddAssemblyContaining<T>();
            return fmb;
        }

        public static FluentModelBuilder DiscoverEntities(this FluentModelBuilder fmb,
            Action<DiscoveryOptions> optionsAction = null)
        {
            //fmb.Entities.Discover(optionsAction);
            return fmb;
        }

        public static FluentModelBuilder DiscoverEntitiesFromSharedAssemblies(this FluentModelBuilder fmb, Action<DiscoveryOptions> optionsAction = null)
        {
            fmb.DiscoverEntities(x =>
            {
                x.FromSharedAssemblies();
                optionsAction?.Invoke(x);
            });
            return fmb;
        }

        public static FluentModelBuilder UsingModelSource<T>(this FluentModelBuilder fmb) where T : IModelSourceApplier
        {
            fmb.Options.ModelSourceApplier = Activator.CreateInstance<T>();
            return fmb;
        }
    }

    public interface IModelSourceApplier
    {
        void Apply(IServiceCollection services);
    }

    public abstract class ModelSourceApplierBase : IModelSourceApplier
    {
        protected abstract Type ServiceType { get; }
        protected abstract Type ImplementationType { get; }
        public void Apply(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Scoped(ServiceType, ImplementationType));
        }
    }
}