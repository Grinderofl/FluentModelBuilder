using System;
using System.Reflection;
using FluentModelBuilder.Core.Contributors.Impl;

namespace FluentModelBuilder.Extensions
{
    public static class OverridesBuilderExtensions
    {
        public static OverridesBuilder Add<TEntity>(this OverridesBuilder builder,
            IEntityTypeOverride<TEntity> entityOverride) where TEntity : class
        {
            return builder.AddContributor(new SingleInstanceOverrideContributor<TEntity>(entityOverride));
        }

        public static OverridesBuilder Add<TOverride>(this OverridesBuilder builder) where TOverride : new()
        {
            return builder.AddContributor(new SingleTypeOverrideContributor(typeof (TOverride)));
        }

        public static OverridesBuilder Discover(this OverridesBuilder builder,
            Action<OverrideDiscoveryContributor> contributorAction = null)
        {
            var contributor = new OverrideDiscoveryContributor();
            contributorAction?.Invoke(contributor);
            return builder.AddContributor(contributor);
        }

        public static OverridesBuilder DiscoverFromSharedAssemblies(this OverridesBuilder builder,
            Action<OverrideDiscoveryContributor> contributorAction = null)
        {
            return builder.Discover(x =>
            {
                x.FromSharedAssemblies();
                contributorAction?.Invoke(x);
            });
        }
    }
}