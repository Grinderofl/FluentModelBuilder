using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FluentModelBuilder.Alterations
{
    /// <summary>
    ///     Provides a way to alter the conventionset used to map the properties of scanned entities
    /// </summary>
    public interface IConventionSetAlteration
    {
        /// <summary>
        ///     Alter the ConventionSet
        /// </summary>
        /// <param name="conventions">ConventionSet to alter</param>
        void Alter(ConventionSet conventions);
    }
}