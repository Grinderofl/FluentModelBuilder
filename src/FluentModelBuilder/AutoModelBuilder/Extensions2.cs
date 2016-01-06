using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentModelBuilder
{
    public static class Extensions2
    {
        public static void ConfigureContext(this IServiceCollection services, Action<FluentModelBuilderConfiguration> configurationAction)
        {
            var conf = new FluentModelBuilderConfiguration();
            configurationAction.Invoke(conf);
            services.AddInstance(conf);
            services.Replace(
                ServiceDescriptor.Singleton<ICoreConventionSetBuilder, AutoCoreConventionSetBuilder>());
        }

        public static bool IsEntityTypeOverrideType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>) &&
                   type.GetGenericArguments().Length > 0;
        }

        public static bool ClosesInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
        }

        public static void ConfigureEntityFramework(this IServiceCollection services, Action<FluentModelBuilderConfiguration> configurationAction)
        {
            var configuration = new FluentModelBuilderConfiguration();
            configurationAction(configuration);
            services.AddInstance(configuration);
            services.Replace(ServiceDescriptor.Singleton<ICoreConventionSetBuilder, AutoCoreConventionSetBuilder>());
        }
    }

    public static class From
    {
        public static AutoModelBuilder Source(ITypeSource source)
        {
            return new AutoModelBuilder().AddTypeSource(source);
        }

        public static AutoModelBuilder Source(ITypeSource source, IEntityAutoConfiguration configuration)
        {
            return new AutoModelBuilder(configuration).AddTypeSource(source);
        }

        public static AutoModelBuilder Assemblies(params Assembly[] assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))));
        }

        public static AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration, params Assembly[] assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);
        }

        public static AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration,
            IEnumerable<Assembly> assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);
        }

        public static AutoModelBuilder Assembly(Assembly assembly)
        {
            return Source(new AssemblyTypeSource(assembly));
        }

        public static AutoModelBuilder Assembly(Assembly assembly, IEntityAutoConfiguration configuration)
        {
            return Source(new AssemblyTypeSource(assembly), configuration);
        }

        public static AutoModelBuilder AssemblyOf<T>()
        {
            return Assembly(typeof(T).GetTypeInfo().Assembly);
        }

        public static AutoModelBuilder AssemblyOf<T>(IEntityAutoConfiguration configuration)
        {
            return Assembly(typeof (T).GetTypeInfo().Assembly, configuration);
        }
    }
}