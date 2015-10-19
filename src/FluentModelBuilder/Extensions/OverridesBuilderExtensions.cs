using System;
using FluentModelBuilder.Contributors.Internal;

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
    }
}