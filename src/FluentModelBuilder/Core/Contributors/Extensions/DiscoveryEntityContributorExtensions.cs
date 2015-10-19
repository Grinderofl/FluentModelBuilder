using System.Reflection;
using FluentModelBuilder.Core.Contributors.Impl;
using FluentModelBuilder.Core.Criteria;

namespace FluentModelBuilder.Core.Contributors.Extensions
{
    public static class DiscoveryEntityContributorExtensions
    {
        /// <summary>
        /// Adds an assembly containing the type to find entities from
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contributor"></param>
        /// <returns></returns>
        public static DiscoveryEntityContributor AssemblyContaining<T>(this DiscoveryEntityContributor contributor)
        {
            return contributor.AddAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Specifies a base type of entity to look for
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contributor"></param>
        /// <returns></returns>
        public static DiscoveryEntityContributor BaseType<T>(this DiscoveryEntityContributor contributor)
        {
            return
                contributor.NotAbstract()
                    .WithCriterion<BaseTypeCriterion>(c => c.AddType(typeof (T).GetTypeInfo()));
        }
    }
}
