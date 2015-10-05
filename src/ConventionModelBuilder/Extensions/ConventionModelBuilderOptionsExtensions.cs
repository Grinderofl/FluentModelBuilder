using System;
using System.Linq;
using ConventionModelBuilder.Conventions;
using ConventionModelBuilder.Conventions.Options;
using ConventionModelBuilder.Options;

namespace ConventionModelBuilder.Extensions
{
    public static class ConventionModelBuilderOptionsExtensions
    {
        /// <summary>
        /// Adds entities from assemblies that correspond to given conditions
        /// </summary>
        /// <param name="options"><see cref="ConventionModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to apply to <see cref="EntityDiscoveryConventionOptions"/></param>
        /// <returns><see cref="ConventionModelBuilderOptions"/></returns>
        public static ConventionModelBuilderOptions AddEntities(this ConventionModelBuilderOptions options, Action<EntityDiscoveryConventionOptions> optionsAction = null)
        {
            var convention = options.Conventions.FirstOrDefault(x => x is EntityDiscoveryConvention) as EntityDiscoveryConvention;
            if (convention == null)
            {
                convention = new EntityDiscoveryConvention();
                options.Conventions.AddFirst(convention);
            }
            
            optionsAction?.Invoke(convention.Options);
            return options;
        }

        /// <summary>
        /// Adds a convention to <see cref="ConventionModelBuilder"/>
        /// </summary>
        /// <param name="options"><see cref="ConventionModelBuilderOptions"/></param>
        /// <param name="convention">Convention to add</param>
        /// <returns><see cref="ConventionModelBuilderOptions"/></returns>
        public static ConventionModelBuilderOptions AddConvention(this ConventionModelBuilderOptions options,
            IModelBuilderConvention convention)
        {
            options.Conventions.AddLast(convention);
            return options;
        }

        /// <summary>
        /// Adds a strongly typed convention to <see cref="ConventionModelBuilder"/>
        /// </summary>
        /// <typeparam name="T">Convention type to add</typeparam>
        /// <param name="options"><see cref="ConventionModelBuilderOptions"/></param>
        /// <returns><see cref="ConventionModelBuilderOptions"/></returns>
        public static ConventionModelBuilderOptions AddConvention<T>(this ConventionModelBuilderOptions options)
            where T : IModelBuilderConvention, new()
        {
            options.AddConvention(new T());
            return options;
        }

        /// <summary>
        /// Adds IEntityTypeOverride to override model builder actions
        /// </summary>
        /// <param name="options"><see cref="ConventionModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to apply to <see cref="EntityTypeOverrideDiscoveryConventionOptions"/></param>
        /// <returns><see cref="ConventionModelBuilderOptions"/></returns>
        public static ConventionModelBuilderOptions AddOverrides(this ConventionModelBuilderOptions options,
            Action<EntityTypeOverrideDiscoveryConventionOptions> optionsAction = null)
        {
            var convention =
                options.Conventions.FirstOrDefault(x => x is EntityTypeOverrideDiscoveryConvention) as
                    EntityTypeOverrideDiscoveryConvention;
            if (convention == null)
            {
                convention = new EntityTypeOverrideDiscoveryConvention();
                options.Conventions.AddLast(convention);
            }

            optionsAction?.Invoke(convention.Options);
            return options;
        }
    }
}