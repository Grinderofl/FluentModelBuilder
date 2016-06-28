using System;
using FluentModelBuilder.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Builder
{
    /// <summary>
    ///     Implement this interface to control how entity discovery behaves.
    ///     Normally you would want to inherit from <see cref="DefaultEntityAutoConfiguration" />, which is configured
    ///     with default settings that can be overridden where alterations are needed.
    /// </summary>
    public interface IEntityAutoConfiguration
    {
        /// <summary>
        ///     Determines whether a type should be automatically mapped. Override to restrict which types are mapped
        ///     in your domain
        /// </summary>
        /// <remarks>
        ///     Normally, you would override this method and restrict via something known, such as the base type
        /// </remarks>
        /// <example>
        ///     return type.GetTypeInfo().IsSubClassOf(typeof(EntityBase));
        /// </example>
        /// <param name="type">Type to map</param>
        /// <returns>Should map type</returns>
        bool ShouldMap(Type type);

        /// <summary>
        ///     Determines whether the mapping should be applied to specified DbContext
        ///     <remarks>
        ///         Normally, you would use this if you had multiple types of DbContexts with different logic for adding entities
        ///     </remarks>
        /// </summary>
        /// <param name="context">DbContext to apply mapping to</param>
        /// <returns>Should apply mapping to given DbContext</returns>
        bool ShouldApplyToContext(DbContext context);

        /// <summary>
        ///     Determines whether the mapping should be applied within given BuilderScope
        ///     <remarks>
        ///         Normally, you would use this to set your mapping configuration to be in the specific scope.
        ///     </remarks>
        /// </summary>
        /// <param name="scope">BuilderScope the mapping process is currently in</param>
        /// <returns>Should apply mapping within given BuilderScope</returns>
        bool ShouldApplyToScope(BuilderScope scope);
    }
}