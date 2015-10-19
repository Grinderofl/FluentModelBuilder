using System.Reflection;
using FluentModelBuilder.Core.Contributors.Impl;
using FluentModelBuilder.Core.Criteria;

namespace FluentModelBuilder.Core.Contributors.Extensions
{
    public static class DiscoveryEntityContributorExtensions
    {
        public static DiscoveryEntityContributor AssemblyContaining<T>(this DiscoveryEntityContributor contributor)
        {
            return contributor.AddAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        public static DiscoveryEntityContributor BaseType<T>(this DiscoveryEntityContributor contributor)
        {
            return
                contributor.NotAbstract()
                    .WithCriterion<BaseTypeCriterion>(c => c.AddType(typeof (T).GetTypeInfo()));
        }
    }
}
