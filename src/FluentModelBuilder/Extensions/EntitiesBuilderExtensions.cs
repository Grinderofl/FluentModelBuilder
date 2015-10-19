using System;
using FluentModelBuilder.Core.Contributors.Impl;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Extensions
{
    public static class EntitiesBuilderExtensions
    {

        /// <summary>
        /// Adds a single entity type to model and configures its mappings
        /// </summary>
        /// <typeparam name="T">Entity Type to add and configure</typeparam>
        /// <param name="builder"><see cref="EntitiesBuilder"/></param>
        /// <param name="builderAction">Entity mapping configuration action</param>
        /// <returns><see cref="EntitiesBuilder"/></returns>
        public static EntitiesBuilder Add<T>(this EntitiesBuilder builder, Action<EntityTypeBuilder<T>> builderAction)
            where T : class
        {
            return builder.AddContributor(new EntityConfigurationContributor<T>(builderAction));
        }

        /// <summary>
        /// Adds a single entity type to model
        /// </summary>
        /// <typeparam name="T">Entity Type to add</typeparam>
        /// <param name="builder"><see cref="EntitiesBuilder"/></param>
        /// <returns><see cref="EntitiesBuilder"/></returns>
        public static EntitiesBuilder Add<T>(this EntitiesBuilder builder)
        {
            return builder.Add(typeof (T));
        }

        /// <summary>
        /// Adds a single entity type to model
        /// </summary>
        /// <param name="builder"><see cref="EntitiesBuilder"/></param>
        /// <param name="type">Type of entity to add</param>
        /// <returns><see cref="EntitiesBuilder"/></returns>
        public static EntitiesBuilder Add(this EntitiesBuilder builder, Type type)
        {
            return builder.WithContributor<ListEntityContributor>(x => x.Add(type));
        }

        /// <summary>
        /// Discovers entities via assembly scanning, using specified criteria
        /// <remarks>Every call will create a new discovery criteria, allowing multiple different discovery setups.</remarks>
        /// </summary>
        /// <param name="builder"><see cref="EntitiesBuilder"/></param>
        /// <param name="contributorAction">Additional configuration for discovery</param>
        /// <returns><see cref="EntitiesBuilder"/></returns>
        public static EntitiesBuilder Discover(this EntitiesBuilder builder,
            Action<DiscoveryEntityContributor> contributorAction = null)
        {
            var contributor = new DiscoveryEntityContributor();
            contributorAction?.Invoke(contributor);
            return builder.AddContributor(contributor);
        }

        /// <summary>
        /// Discovers entities via assembly scanning, using specified criteria and finding from shared assemblies by default
        /// <remarks>Every call will create a new discovery criteria, allowing multiple different discovery setups.</remarks>
        /// </summary>
        /// <param name="builder"><see cref="EntitiesBuilder"/></param>
        /// <param name="contributorAction">Additional configuration for discovery</param>
        /// <returns><see cref="EntitiesBuilder"/></returns>
        public static EntitiesBuilder DiscoverFromSharedAssemblies(this EntitiesBuilder builder,
            Action<DiscoveryEntityContributor> contributorAction = null)
        {
            return builder.Discover(x =>
            {
                x.FromSharedAssemblies();
                contributorAction?.Invoke(x);
            });
        }
    }
}