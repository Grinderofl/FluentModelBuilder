using System;
using System.Reflection;
using FluentModelBuilder.Core.Criteria;

namespace FluentModelBuilder.Core.Contributors.Extensions
{
    public static class DiscoveryContributorExtensions
    {
        /// <summary>
        /// Finds types with specified base type
        /// </summary>
        /// <typeparam name="T"><see cref="DiscoveryContributorBase"/></typeparam>
        /// <param name="contributor"><see cref="DiscoveryContributorBase"/></param>
        /// <param name="type">Base type to look for</param>
        /// <returns></returns>
        public static T BaseType<T>(this T contributor, Type type) where T : DiscoveryContributorBase<T>
        {
            return
                contributor.NotAbstract()
                    .WithCriterion<BaseTypeCriterion>(c => c.AddType(type.GetTypeInfo()));
        }

        /// <summary>
        /// Finds types that are not abstract
        /// </summary>
        /// <typeparam name="T"><see cref="DiscoveryContributorBase"/></typeparam>
        /// <param name="contributor"><see cref="DiscoveryContributorBase"/></param>
        /// <returns></returns>
        public static T NotAbstract<T>(this T contributor) where T : DiscoveryContributorBase<T>
        {
            return contributor.WithCriterion<NonAbstractCriterion>();
        }

        /// <summary>
        /// Specifies a namespace criterion
        /// </summary>
        /// <typeparam name="T"><see cref="DiscoveryContributorBase"/></typeparam>
        /// <param name="contributor"><see cref="DiscoveryContributorBase"/></param>
        /// <param name="namespaceAction">Condition on namespace</param>
        /// <returns></returns>
        public static T Namespace<T>(this T contributor,
            Func<string, bool> namespaceAction) where T : DiscoveryContributorBase<T>
        {
            return contributor.AddCriterion(new ExpressionCriterion(t => namespaceAction(t.Namespace)));
        }

        /// <summary>
        /// Specifies a custom criterion
        /// </summary>
        /// <typeparam name="T"><see cref="DiscoveryContributorBase"/></typeparam>
        /// <param name="contributor"><see cref="DiscoveryContributorBase"/></param>
        /// <param name="typeExpression">Criterion expression</param>
        /// <returns></returns>
        public static T When<T>(this T contributor,
            Func<TypeInfo, bool> typeExpression) where T : DiscoveryContributorBase<T>
        {
            return contributor.AddCriterion(new ExpressionCriterion(typeExpression));
        }
    }
}