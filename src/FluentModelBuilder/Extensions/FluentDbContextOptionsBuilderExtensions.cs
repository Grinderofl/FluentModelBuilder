using System;
using System.Collections.Generic;
using System.Reflection;
using FluentModelBuilder.Core.Contributors.Impl;

namespace FluentModelBuilder.Extensions
{
    public static class FluentDbContextOptionsBuilderExtensions
    {
        public static FluentDbContextOptionsBuilder WithExtension<T>(this FluentDbContextOptionsBuilder builder)
            where T : IBuilderExtension, new()
        {
            return builder.WithExtension(new T());
        }

        /// <summary>
        /// Discovers entities via assembly scanning, using specified criteria
        /// <remarks>Every call will create a new discovery criteria, allowing multiple different discovery setups.</remarks>
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="builderAction">Additional discovery configuration</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder DiscoverEntities(this FluentDbContextOptionsBuilder builder,
            Action<EntityDiscoveryContributor> builderAction = null)
        {
            return builder.Entities(e => e.Discover(builderAction));
        }

        /// <summary>
        /// Discovers entities via assembly scanning, using specified criteria and finding from shared assemblies by default
        /// <remarks>Every call will create a new discovery criteria, allowing multiple different discovery setups.</remarks>
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="builderAction">Additional discovery configuration</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder DiscoverEntitiesFromSharedAssemblies(
            this FluentDbContextOptionsBuilder builder, Action<EntityDiscoveryContributor> builderAction = null)
        {
            return builder.DiscoverEntities(e =>
            {
                e.FromSharedAssemblies();
                builderAction?.Invoke(e);
            });
        }

        /// <summary>
        /// Discovers and activates types of IEntityTypeOverride`[] via assembly scanning, using specified criteria
        /// <remarks>Every call will create a new discovery criteria, allowing multiple different discovery setups.</remarks>
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="builderAction">Additional discovery configuration</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder DiscoverOverrides(this FluentDbContextOptionsBuilder builder,
            Action<OverrideDiscoveryContributor> builderAction = null)
        {
            return builder.Overrides(o => o.Discover(builderAction));
        }

        /// <summary>
        /// Discovers and activates types of IEntityTypeOverride`[] via assembly scanning, 
        /// using specified criteria and finding from shared assemblies by default
        /// <remarks>Every call will create a new discovery criteria, allowing multiple different discovery setups.</remarks>
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="builderAction">Additional discovery configuration</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder DiscoverOverridesFromSharedAssemblies(
            this FluentDbContextOptionsBuilder builder, Action<OverrideDiscoveryContributor> builderAction = null)
        {
            return builder.DiscoverOverrides(o =>
            {
                o.FromSharedAssemblies();
                builderAction?.Invoke(o);
            });
        }

        /// <summary>
        /// Adds a single shared assembly to scan entities and/or overrides from
        /// <remarks>Duplicate assemblies won't be added</remarks>
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="assembly">Assembly to add</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder AddAssembly(this FluentDbContextOptionsBuilder builder,
            Assembly assembly)
        {
            return builder.Assemblies(a => a.AddAssembly(assembly));
        }

        /// <summary>
        /// Adds multiple shared assemblies to scan entities and/or overrides from
        /// <remarks>Duplicate assemblies won't be added</remarks>
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="assemblies">Assemblies to add</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder AddAssemblies(this FluentDbContextOptionsBuilder builder,
            IEnumerable<Assembly> assemblies)
        {
            return builder.Assemblies(a => a.Add(assemblies));
        }

        /// <summary>
        /// Adds a single shared assembly which contains the specified type
        /// to scan entities and/or overrides from
        /// <remarks>Duplicate assemblies won't be added</remarks>
        /// </summary>
        /// <typeparam name="T">Type of which' containing assembly to add</typeparam>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder AddAssemblyContaining<T>(this FluentDbContextOptionsBuilder builder)
        {
            return builder.Assemblies(a => a.AddAssemblyContaining<T>());
        }
    }
}