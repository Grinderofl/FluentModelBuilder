using System;
using FluentModelBuilder.Conventions.EntityConvention.Options;
using FluentModelBuilder.Conventions.OverrideConvention;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Conventions.Core.Options.Extensions
{
    public static class OverrideConventionOptionsExtensions
    {
        public static OverrideConventionOptions Discover(this OverrideConventionOptions options,
            Action<EntityTypeOverrideDiscoveryConventionOptions> optionsAction = null)
        {
            var conventionOptions = new EntityTypeOverrideDiscoveryConventionOptions();
            optionsAction?.Invoke(conventionOptions);
            options.ModelBuilderConventions.Add(new OverrideDiscoveryConvention(conventionOptions));
            return options;
        }

        public static OverrideConventionOptions Add<T>(this OverrideConventionOptions options)
        {
            options.ModelBuilderConventions.Add(new SingleOverrideConvention<T>());
            return options;
        }
    }
}
