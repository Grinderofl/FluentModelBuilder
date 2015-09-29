using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ConventionModelBuilder.Conventions.Criteria;
using ConventionModelBuilder.Extensions;

namespace ConventionModelBuilder.Conventions.Options.Extensions
{
    public static class EntityDiscoveryConventionOptionsExtensions
    {
        public static EntityDiscoveryConventionOptions WithBaseType(this EntityDiscoveryConventionOptions options,
            Type type)
        {
            var baseTypeCriteria =
                options.Criterias.FirstOrDefault(x => x is BaseTypeCriteria && ((BaseTypeCriteria) x).Type == type);
            if (baseTypeCriteria == null)
            {
                // Assumes we only want non-abstract types
                var abstractCriteria = new ExpressionCriteria(x => !x.IsAbstract);
                options.Criterias.Add(abstractCriteria);

                baseTypeCriteria = new BaseTypeCriteria(type);
                options.Criterias.Add(baseTypeCriteria);
            }
            return options;
        }

        public static EntityDiscoveryConventionOptions WithBaseType<T>(this EntityDiscoveryConventionOptions options)
            where T : class
        {
            return options.WithBaseType(typeof (T));
        }
    }
}
