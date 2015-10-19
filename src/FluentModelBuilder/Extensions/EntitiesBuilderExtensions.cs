using System;
using FluentModelBuilder.Contributors.Internal;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Extensions
{
    public static class EntitiesBuilderExtensions
    {

        public static EntitiesBuilder Add<T>(this EntitiesBuilder builder, Action<EntityTypeBuilder<T>> builderAction)
            where T : class
        {
            return builder.AddContributor(new EntityConfigurationContributor<T>(builderAction));
        }

        public static EntitiesBuilder Add<T>(this EntitiesBuilder builder)
        {
            return builder.Add(typeof (T));
        }

        public static EntitiesBuilder Add(this EntitiesBuilder builder, Type type)
        {
            return builder.WithContributor<ListEntityContributor>(x => x.Add(type));
        }

        public static EntitiesBuilder Discover(this EntitiesBuilder builder,
            Action<DiscoveryEntityContributor> contributorAction = null, AssembliesBuilder assembliesBuilder = null)
        {
            var contributor = new DiscoveryEntityContributor();
            contributorAction?.Invoke(contributor);
            return builder.AddContributor(contributor);
        }
    }
}