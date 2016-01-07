using System;
using FluentModelBuilder.Configuration;

namespace FluentModelBuilder.Builder
{
    /// <summary>
    /// Implement this interface to control how entity discovery behaves.
    /// Normally you would want to inherit from <see cref="DefaultEntityAutoConfiguration"/>, which is configured
    /// with default settings that can be overridden where alterations are needed.
    /// </summary>
    public interface IEntityAutoConfiguration
    {
        /// <summary>
        /// Determines whether a type should be automatically mapped. Override to restrict which types are mapped
        /// in your domain
        /// </summary>
        /// <remarks>
        /// Normally, you would override this method and restrict via something known, such as the base type
        /// </remarks>
        /// <example>
        /// return type.GetTypeInfo().IsSubClassOf(typeof(EntityBase));
        /// </example>
        /// <param name="type">Type to map</param>
        /// <returns>Should map type</returns>
        bool ShouldMap(Type type);    
    }
}