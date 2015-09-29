using System;
using System.Linq;
using ConventionModelBuilder.Conventions;
using ConventionModelBuilder.Conventions.Options;
using ConventionModelBuilder.Options;

namespace ConventionModelBuilder.Extensions
{
    public static class ConventionModelBuilderOptionsExtensions
    {
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

        public static ConventionModelBuilderOptions AddConvention(this ConventionModelBuilderOptions options,
            IModelBuilderConvention convention)
        {
            options.Conventions.AddLast(convention);
            return options;
        }

        public static ConventionModelBuilderOptions AddConvention<T>(this ConventionModelBuilderOptions options)
            where T : IModelBuilderConvention, new()
        {
            options.AddConvention(new T());
            return options;
        }

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