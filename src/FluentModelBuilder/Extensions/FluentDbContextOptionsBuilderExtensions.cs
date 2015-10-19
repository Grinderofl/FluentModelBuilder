using System;
using System.Reflection;
using FluentModelBuilder.Core.Contributors.Impl;

namespace FluentModelBuilder.Extensions
{
    public static class FluentDbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Adds an extension to Fluent Builder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static FluentDbContextOptionsBuilder WithExtension<T>(this FluentDbContextOptionsBuilder builder)
            where T : IBuilderExtension, new()
        {
            return builder.WithExtension(new T());
        }

        public static FluentDbContextOptionsBuilder DiscoverEntities(this FluentDbContextOptionsBuilder builder,
            Action<DiscoveryEntityContributor> builderAction = null)
        {
            return builder.Entities(e => e.Discover(builderAction));
        }

        public static FluentDbContextOptionsBuilder DiscoverEntitiesFromSharedAssemblies(
            this FluentDbContextOptionsBuilder builder, Action<DiscoveryEntityContributor> builderAction = null)
        {
            return builder.DiscoverEntities(e =>
            {
                e.FromSharedAssemblies();
                builderAction?.Invoke(e);
            });
        }

        public static FluentDbContextOptionsBuilder DiscoverOverrides(this FluentDbContextOptionsBuilder builder,
            Action<DiscoveryOverrideContributor> builderAction = null)
        {
            return builder.Overrides(o => o.Discover(builderAction));
        }

        public static FluentDbContextOptionsBuilder DiscoverOverridesFromSharedAssemblies(
            this FluentDbContextOptionsBuilder builder, Action<DiscoveryOverrideContributor> builderAction = null)
        {
            return builder.DiscoverOverrides(o =>
            {
                o.FromSharedAssemblies();
                builderAction?.Invoke(o);
            });
        }

        public static FluentDbContextOptionsBuilder AddAssembly(this FluentDbContextOptionsBuilder builder,
            Assembly assembly)
        {
            return builder.Assemblies(a => a.AddAssembly(assembly));
        }

        public static FluentDbContextOptionsBuilder AddAssemblyContaining<T>(this FluentDbContextOptionsBuilder builder)
        {
            return builder.Assemblies(a => a.AddAssemblyContaining<T>());
        }
    }
}