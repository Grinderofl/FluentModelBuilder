using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentModelBuilder.Contributors.Internal;
using FluentModelBuilder.Contributors.Internal.Criteria;

namespace FluentModelBuilder.Extensions
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
                contributor.WithCriterion<NonAbstractCriterion>()
                    .WithCriterion<BaseTypeCriterion>(c => c.AddType(typeof (T).GetTypeInfo()));
        }

        public static DiscoveryEntityContributor Namespace(this DiscoveryEntityContributor contributor,
            Func<string, bool> namespaceAction)
        {
            return contributor.AddCriterion(new ExpressionCriterion(t => namespaceAction(t.Namespace)));
        }
    }
}
