using System;
using FluentModelBuilder.Conventions.Entities.Options;
using FluentModelBuilder.Conventions.Overrides;
using FluentModelBuilder.Conventions.Overrides.Options;

namespace FluentModelBuilder.Conventions.Assemblies.Options.Extensions
{
    public static class OverrideConventionOptionsExtensions
    {
        /// <summary>
        /// Discovers overrides from specified assemblies
        /// </summary>
        /// <param name="options"><see cref="OverrideConventionOptions"/></param>
        /// <param name="optionsAction">Actions to perform during discovery</param>
        /// <returns><see cref="OverrideConventionOptions"/></returns>
        public static OverrideConventionOptions Discover(this OverrideConventionOptions options,
            Action<EntityTypeOverrideDiscoveryConventionOptions> optionsAction = null)
        {
            var conventionOptions = new EntityTypeOverrideDiscoveryConventionOptions();
            optionsAction?.Invoke(conventionOptions);
            options.ModelBuilderConventions.Add(new OverrideDiscoveryConvention(conventionOptions));
            return options;
        }

        /// <summary>
        /// Adds single IEntityTypeOverride for configuring model
        /// </summary>
        /// <typeparam name="T">Type of override to add</typeparam>
        /// <param name="options"><see cref="OverrideConventionOptions"/></param>
        /// <returns><see cref="OverrideConventionOptions"/></returns>
        public static OverrideConventionOptions Add<T>(this OverrideConventionOptions options)
        {
            options.ModelBuilderConventions.Add(new SingleOverrideConvention<T>());
            return options;
        }
    }
}
