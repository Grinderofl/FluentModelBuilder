using System;
using FluentModelBuilder.Conventions.Core.Options.Extensions;
using FluentModelBuilder.Conventions.EntityConvention.Options;
using FluentModelBuilder.Options;
using FluentModelBuilder.Options.Extensions;

namespace FluentModelBuilder.Extensions
{
    public static class FluentModelBuilderOptionsOverridesExtensions
    {
        public static FluentModelBuilderOptions DiscoverOverrides(this FluentModelBuilderOptions options,
            Action<EntityTypeOverrideDiscoveryConventionOptions> optionsAction = null)
        {
            return options.Overrides(x => x.Discover(optionsAction));
        }

        public static FluentModelBuilderOptions DiscoverOverridesFromCommonAssemblies(
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