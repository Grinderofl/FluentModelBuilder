using System;
using System.Linq;
using ConventionModelBuilder.Conventions.Criteria;

namespace ConventionModelBuilder.Conventions.Options.Extensions
{
    public static class EntityDiscoveryConventionOptionsExtensions
    {
        /// <summary>
        /// Adds a base type criteria to <see cref="EntityDiscoveryConvention"/>
        /// </summary>
        /// <param name="options"><see cref="EntityTypeOverrideDiscoveryConventionOptions"/></param>
        /// <param name="type">Specified entity base type</param>
        /// <returns><see cref="EntityDiscoveryConventionOptions"/></returns>
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

        /// <summary>
        /// Adds a base type criteria to <see cref="EntityDiscoveryConvention"/>
        /// </summary>
        /// <typeparam name="T">Type of entity base to add</typeparam>
        /// <param name="options"><see cref="EntityTypeOverrideDiscoveryConventionOptions"/></param>
        /// <returns><see cref="EntityDiscoveryConventionOptions"/></returns>
        public static EntityDiscoveryConventionOptions WithBaseType<T>(this EntityDiscoveryConventionOptions options)
            where T : class
        {
            return options.WithBaseType(typeof (T));
        }
    }
}
