using System;
using System.Reflection;
using FluentModelBuilder.Contributors.Internal;
using FluentModelBuilder.Contributors.Internal.Criteria;

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
            Action<DiscoveryOverrideContributor> contributorAction = null, AssembliesBuilder assembliesBuilder = null)
        {
            var contributor = assembliesBuilder == null
                ? new DiscoveryOverrideContributor()
                : new DiscoveryOverrideContributor(assembliesBuilder);
            contributorAction?.Invoke(contributor);
            return builder.AddContributor(contributor);
        }

        public static DiscoveryEntityContributor When(this DiscoveryEntityContributor contributor,
            Func<TypeInfo, bool> typeExpression)
        {
            return contributor.AddCriterion(new ExpressionCriterion(typeExpression));
        }
    }
}