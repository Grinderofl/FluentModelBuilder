using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentModelBuilder.Contributors.Internal;
using FluentModelBuilder.Contributors.Internal.Criteria;

namespace FluentModelBuilder.Extensions
{
    public static class DiscoveryOverrideContributorExtensions
    {
        public static DiscoveryOverrideContributor AssemblyContaining<T>(this DiscoveryOverrideContributor contributor)
        {
            return contributor.AddAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        public static DiscoveryOverrideContributor Namespace(this DiscoveryOverrideContributor contributor,
            Func<string, bool> namespaceAction)
        {
            return contributor.AddCriterion(new ExpressionCriterion(t => namespaceAction(t.Namespace)));
        }
    }
}
