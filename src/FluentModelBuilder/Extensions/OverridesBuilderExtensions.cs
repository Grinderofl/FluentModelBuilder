using System;
using System.Reflection;
using FluentModelBuilder.Contributors.Core;

namespace FluentModelBuilder.Extensions
{
    public static class OverridesBuilderExtensions
    {
        public static OverridesBuilder Add<TEntity>(this OverridesBuilder builder,
            IEntityTypeOverride<TEntity> entityOverride) where TEntity : class
        {
            return builder.AddContributor(new SingleOverrideContributor<TEntity>(entityOverride));
        }

        public static OverridesBuilder Add<TOverride>(this OverridesBuilder builder) where TOverride : new()
        {
            return builder.AddContributor(new SingleOverrideContributor(typeof (TOverride)));
        }

        public static OverridesBuilder Discover(this OverridesBuilder builder,
            Action<DiscoveryOverrideContributor> contributorAction = null)
        {
            var contributor = new DiscoveryOverrideContributor();
            contributorAction?.Invoke(contributor);
            return builder.AddContributor(contributor);
        }
    }
}