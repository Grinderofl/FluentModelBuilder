using System.Reflection;
using FluentModelBuilder.Core.Contributors.Impl;
using FluentModelBuilder.Core.Criteria;

namespace FluentModelBuilder.Core.Contributors.Extensions
{
    public static class DiscoveryOverrideContributorExtensions
    {
        public static DiscoveryOverrideContributor AssemblyContaining<T>(this DiscoveryOverrideContributor contributor)
        {
            return contributor.AddAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        public static DiscoveryOverrideContributor BaseType<T>(this DiscoveryOverrideContributor contributor)
        {
            return
                contributor.NotAbstract()
                    .WithCriterion<BaseTypeCriterion>(c => c.AddType(typeof(T).GetTypeInfo()));
        }
    }
}
