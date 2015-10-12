using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Conventions.Core.Criteria;
using FluentModelBuilder.Sources.Assemblies;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    /// <summary>
    /// Builds model using conventions
    /// </summary>
    public class FluentModelBuilder : AbstractCoreDescriptor
    {
        public override void ApplyServices(IServiceCollection services)
        {

        }
    }

    public static class DbContextOptionsExtensions
    {
        public static FluentModelBuilder BuildModel(this DbContextOptionsBuilder options)
        {
            var extension = options.Options.FindExtension<FluentModelBuilderExtension>();
            if (extension == null)
            {
                extension = new FluentModelBuilderExtension();
                options.Options.WithExtension(extension);
            }
            return extension.Builder;
        }
    }

    public class OverrideCoreDescriptor : AbstractCoreDescriptor
    {
        public IList<IDescriptor> Descriptors { get; } = new List<IDescriptor>();

        public override void ApplyServices(IServiceCollection services)
        {
            
        }
    }

    public abstract class AbstractCoreDescriptor : CoreDescriptorBase
    {
        public virtual EntityCoreDescriptor Entities => WithCoreDescriptor<EntityCoreDescriptor>();
        public virtual AssemblyCoreDescriptor Assemblies => WithCoreDescriptor<AssemblyCoreDescriptor>();
        public virtual OverrideCoreDescriptor Overrides => WithCoreDescriptor<OverrideCoreDescriptor>();
    }

    public abstract class CoreDescriptorBase : ICoreDescriptor
    {
        protected IList<ICoreDescriptor> CoreDescriptors = new List<ICoreDescriptor>();

        public abstract void ApplyServices(IServiceCollection services);

        public T WithCoreDescriptor<T>() where T : ICoreDescriptor, new()
        {
            var descriptor = CoreDescriptors.SingleOrDefault(x => x is T);
            if (descriptor == null)
            {
                descriptor = ActivatorUtilities.CreateInstance<T>(null, CoreDescriptors);
                CoreDescriptors.Add(descriptor);
            }

            return (T)descriptor;
        }
    }

    public class EntityCoreDescriptor : AbstractCoreDescriptor
    {
        public IList<IDescriptor> EntityDescriptors { get; } = new List<IDescriptor>();

        public override void ApplyServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }

    public static class EntityCoreDescriptorExtensions
    {
        public static EntityCoreDescriptor Discover(this EntityCoreDescriptor descriptor, Action<DiscoveryOptions> optionsAction = null)
        {
            var discoveryDescriptor = new DiscoveryDescriptor();
            optionsAction?.Invoke(discoveryDescriptor.Options);
            descriptor.EntityDescriptors.Add(discoveryDescriptor);
            return descriptor;
        }

        public static EntityCoreDescriptor Add<T>(this EntityCoreDescriptor descriptor)
        {
            descriptor.EntityDescriptors.Add(new SingleEntityDescriptor(typeof(T)));
            return descriptor;
        }

        public static EntityCoreDescriptor Add<T>(this EntityCoreDescriptor descriptor,
            Action<EntityTypeBuilder<T>> builderAction) where T : class
        {
            descriptor.EntityDescriptors.Add(new EntityConfigurationDescriptor<T>(builderAction));
            return descriptor;
        }
    }

    public class AssemblyCoreDescriptor : AbstractCoreDescriptor
    {
        public AssemblyCoreDescriptorOptions Options { get; } = new AssemblyCoreDescriptorOptions();
        public override void ApplyServices(IServiceCollection services)
        {
            services.AddInstance<ISharedAssemblySource>(new SharedAssemblySource(Options.Assemblies));
        }
    }

    public class AssemblyCoreDescriptorOptions
    {
        public IList<Assembly> Assemblies { get; } = new List<Assembly>();
    }

    public static class AssemblyCoreDescriptorExtensions
    {
        public static AssemblyCoreDescriptor AddAssemblyContaining<T>(this AssemblyCoreDescriptor descriptor)
        {
            var assembly = typeof (T).GetTypeInfo().Assembly;
            return descriptor.AddAssembly(assembly);
        }

        public static AssemblyCoreDescriptor Add(this AssemblyCoreDescriptor descriptor, Assembly assembly)
        {
            descriptor.Options.Assemblies.AddIfNotExists(assembly);
            return descriptor;
        }

        public static AssemblyCoreDescriptor AddAssembly(this AssemblyCoreDescriptor descriptor, Assembly assembly)
        {
            descriptor.Options.Assemblies.AddIfNotExists(assembly);
            return descriptor;
        }

        public static AssemblyCoreDescriptor Add(this AssemblyCoreDescriptor descriptor, Action<AssemblyCoreDescriptorOptions> optionsAction = null)
        {
            optionsAction?.Invoke(descriptor.Options);
            return descriptor;
        }
    }

    public static class AssemblyCoreDescriptorOptionsExtensions
    {
        public static AssemblyCoreDescriptorOptions Single(this AssemblyCoreDescriptorOptions options, Assembly assembly)
        {
            options.Assemblies.AddIfNotExists(assembly);
            return options;
        }

        public static AssemblyCoreDescriptorOptions Containing(this AssemblyCoreDescriptorOptions options, Type type)
        {
            return options.Single(type.GetTypeInfo().Assembly);
        }

        public static AssemblyCoreDescriptorOptions Containing<T>(this AssemblyCoreDescriptorOptions options)
        {
            return options.Containing(typeof(T));
        }
    }

    public static class AssemblyExtensions
    {
        public static void AddIfNotExists<T>(this IList<T> list, T item)
        {
            if(!list.Contains(item))
                list.Add(item);
        }
    }

    public class DiscoveryOptions
    {
        public IList<ITypeInfoCriteria> Criterias { get; } = new List<ITypeInfoCriteria>();
        public IList<Assembly> Assemblies { get; } = new List<Assembly>();

        public bool UseSharedAssemblies { get; set; }
    }

    public class GenericTypeOverride<T> : IEntityTypeOverride<T> where T : class
    {
        private readonly Action<EntityTypeBuilder<T>> _mapping;

        public GenericTypeOverride(Action<EntityTypeBuilder<T>> mapping)
        {
            _mapping = mapping;
        }

        public void Configure(EntityTypeBuilder<T> mapping)
        {
            _mapping(mapping);
        }
    }

    public static class FluentModelBuilderExtensions2
    {
        public static EntityCoreDescriptor Entities(this AbstractCoreDescriptor fmb)
        {
            return fmb.WithCoreDescriptor<EntityCoreDescriptor>();
        }

        public static AssemblyCoreDescriptor Assemblies(this AbstractCoreDescriptor fmb)
        {
            return fmb.WithCoreDescriptor<AssemblyCoreDescriptor>();
        }

        public static FluentModelBuilder AddAssemblyContaining<T>(this FluentModelBuilder fmb)
        {
            fmb.Assemblies().AddAssemblyContaining<T>();
            return fmb;
        }

        public static FluentModelBuilder DiscoverEntities(this FluentModelBuilder fmb,
            Action<DiscoveryOptions> optionsAction = null)
        {
            fmb.Entities().Discover(optionsAction);
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
    }

    public class SharedAssemblySource : ISharedAssemblySource
    {
        private readonly IList<Assembly> _assemblies;

        public SharedAssemblySource(IList<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            return _assemblies;
        }
    }

    public interface ISharedAssemblySource : IAssemblySource
    {
    }

    public class DiscoveryTypeSource : ITypeSource
    {
        private readonly IList<Assembly> _assemblies;
        private readonly IList<ITypeInfoCriteria> _criterias;

        public DiscoveryTypeSource(IList<Assembly> assemblies, IList<ITypeInfoCriteria> criterias)
        {
            _assemblies = assemblies;
            _criterias = criterias;
        }

        public IEnumerable<Type> GetTypes()
        {
            return _assemblies
                .SelectMany(x => x.GetExportedTypes())
                .Where(x => _criterias.Any(c => c.IsSatisfiedBy(x.GetTypeInfo())));
        }
    }

    public class SingleTypeSource : ITypeSource
    {
        private readonly Type _type;

        public SingleTypeSource(Type type)
        {
            _type = type;
        }

        public IEnumerable<Type> GetTypes()
        {
            yield return _type;
        }
    }

    public interface ICoreDescriptor : IDescriptor
    {
        T WithCoreDescriptor<T>() where T : ICoreDescriptor, new();
        //void ApplyServices(IServiceCollection services);
    }

    public interface IDescriptor
    {
        void ApplyServices(IServiceCollection services);
    }


}
