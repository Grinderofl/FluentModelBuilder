using FluentModelBuilder.Options.Extensions;

namespace FluentModelBuilder.Conventions.Entities.Options.Extensions
{
    public static class EntityTypeOverrideDiscoveryConventionOptionsExtensions
    {
        public static EntityTypeOverrideDiscoveryConventionOptions FromAssemblyContaining<T>(
            this EntityTypeOverrideDiscoveryConventionOptions options) where T : new()
        {
            return options.FromAssemblyContaining<EntityTypeOverrideDiscoveryConventionOptions, T>();
        }
    }
}