using System;

namespace FluentModelBuilder
{
    public static class EntitiesBuilderExtensions
    {
        public static EntitiesBuilder Add<T>(this EntitiesBuilder builder)
        {
            return builder.Add(typeof (T));
        }

        public static EntitiesBuilder Add(this EntitiesBuilder builder, Type type)
        {
            return builder.WithContributor<ListEntityContributor>(x => x.Add(type));
        }

        public static EntitiesBuilder Discover(this EntitiesBuilder builder,
            Action<DiscoveryEntityContributor> contributorAction = null)
        {
            return builder.WithContributor<DiscoveryEntityContributor>(contributorAction);
        }
    }
}