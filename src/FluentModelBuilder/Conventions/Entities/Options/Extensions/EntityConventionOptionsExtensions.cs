using System;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Conventions.Entities.Options.Extensions
{
    public static class EntityConventionOptionsExtensions
    {
        public static EntityConventionOptions Discover(this EntityConventionOptions options,
            Action<EntityDiscoveryConventionOptions> optionsAction = null)
        {
            var conventionOptions = new EntityDiscoveryConventionOptions();
            optionsAction?.Invoke(conventionOptions);
            options.ModelBuilderConventions.Add(new EntityDiscoveryConvention(conventionOptions));
            return options;
        }

        public static EntityConventionOptions Add<T>(this EntityConventionOptions options)
        {
            options.ModelBuilderConventions.Add(new SingleEntityConvention(typeof(T)));
            return options;
        }

        public static EntityConventionOptions Add<T>(this EntityConventionOptions options,
            Action<EntityTypeBuilder<T>> builderAction) where T : class
        {
            options.ModelBuilderConventions.Add(new SingleEntityConfigurationConvention<T>(builderAction));
            return options;
        }

        public static EntityConventionOptions Remove<T>(this EntityConventionOptions options)
        {
            options.ModelBuilderConventions.Add(new RemoveEntityConvention<T>());
            return options;
        }
    }
}
