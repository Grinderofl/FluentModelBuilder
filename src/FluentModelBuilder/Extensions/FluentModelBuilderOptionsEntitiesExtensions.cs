using System;
using FluentModelBuilder.Conventions.Core;
using FluentModelBuilder.Conventions.Core.Options.Extensions;
using FluentModelBuilder.Conventions.EntityConvention;
using FluentModelBuilder.Conventions.EntityConvention.Options;
using FluentModelBuilder.Options;
using FluentModelBuilder.Options.Extensions;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Extensions
{
    public static class FluentModelBuilderOptionsEntitiesExtensions
    {
        public static FluentModelBuilderOptions AddEntity<T>(this FluentModelBuilderOptions options)
        {
            var convention = options.WithConvention<EntityConvention>();
            convention.Options.ModelBuilderConventions.Add(new SingleEntityConvention(typeof (T)));
            return options;
        }

        public static FluentModelBuilderOptions AddEntity<T>(this FluentModelBuilderOptions options, Action<EntityTypeBuilder<T>> configurationAction) where T : class
        {
            var convention = options.WithConvention<EntityConvention>();
            convention.Options.ModelBuilderConventions.Add(new SingleEntityConfigurationConvention<T>(configurationAction));
            return options;
        }

        public static FluentModelBuilderOptions DiscoverEntities(this FluentModelBuilderOptions options,
            Action<EntityDiscoveryConventionOptions> optionsAction = null)
        {
            return options.Entities(x => x.Discover(optionsAction));
        }

        public static FluentModelBuilderOptions DiscoverEntitiesFromCommonAssemblies(
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