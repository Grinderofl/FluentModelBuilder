using System;
using FluentModelBuilder.Conventions.Entities;
using FluentModelBuilder.Conventions.Entities.Options;
using FluentModelBuilder.Conventions.Entities.Options.Extensions;
using FluentModelBuilder.Options;
using FluentModelBuilder.Options.Extensions;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Extensions
{
    public static class FluentModelBuilderOptionsEntitiesExtensions
    {

        /// <summary>
        /// Adds single entity to model
        /// </summary>
        /// <typeparam name="T">Type of entity to add</typeparam>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddEntity<T>(this FluentModelBuilderOptions options)
        {
            var convention = options.WithConvention<EntityConvention>();
            convention.Options.ModelBuilderConventions.Add(new SingleEntityConvention(typeof (T)));
            return options;
        }

        /// <summary>
        /// Adds to and configures single entity on model
        /// </summary>
        /// <typeparam name="T">Type of entity to add and configure</typeparam>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="configurationAction">Configuration to perform on entity</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddEntity<T>(this FluentModelBuilderOptions options, Action<EntityTypeBuilder<T>> configurationAction) where T : class
        {
            var convention = options.WithConvention<EntityConvention>();
            convention.Options.ModelBuilderConventions.Add(new SingleEntityConfigurationConvention<T>(configurationAction));
            return options;
        }

        /// <summary>
        /// Discovers entities to add to model
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to perform during discovery</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions DiscoverEntities(this FluentModelBuilderOptions options,
            Action<EntityDiscoveryConventionOptions> optionsAction = null)
        {
            return options.Entities(x => x.Discover(optionsAction));
        }

        /// <summary>
        /// Discovers entities from common assembly convention
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to perform during discovery</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions DiscoverEntitiesFromAssemblyConvention(
            this FluentModelBuilderOptions options, Action<EntityDiscoveryConventionOptions> optionsAction = null)
        {
            return options.DiscoverEntities(discover =>
            {
                discover.FromAssemblyConvention(options);
                optionsAction?.Invoke(discover);
            });
        }
    }
}