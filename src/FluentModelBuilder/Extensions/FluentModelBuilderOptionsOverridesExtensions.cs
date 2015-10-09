using System;
using FluentModelBuilder.Conventions.Assemblies.Options.Extensions;
using FluentModelBuilder.Conventions.Entities.Options;
using FluentModelBuilder.Options;
using FluentModelBuilder.Options.Extensions;

namespace FluentModelBuilder.Extensions
{
    public static class FluentModelBuilderOptionsOverridesExtensions
    {
        /// <summary>
        /// Discovers IEntityTypeOverrides
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to perform during discovery</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions DiscoverOverrides(this FluentModelBuilderOptions options,
            Action<EntityTypeOverrideDiscoveryConventionOptions> optionsAction = null)
        {
            return options.Overrides(x => x.Discover(optionsAction));
        }

        /// <summary>
        /// Discovers IEntityTypeOverrides from common assembly convention
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to perform during discovery</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions DiscoverOverridesFromAssemblyConvention(
            this FluentModelBuilderOptions options,
            Action<EntityTypeOverrideDiscoveryConventionOptions> optionsAction = null)
        {
            return options.DiscoverOverrides(discover =>
            {
                discover.FromAssemblyConvention(options);
                optionsAction?.Invoke(discover);
            });
        }
    }
}