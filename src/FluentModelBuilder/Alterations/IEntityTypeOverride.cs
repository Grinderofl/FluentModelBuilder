using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.Alterations
{
    /// <summary>
    ///     Provides a way to override the configuration of a single entity
    /// </summary>
    /// <remarks>You would use this instead of &lt; EF6's EntityTypeConfiguration class</remarks>
    /// <typeparam name="T">Type of entity to override</typeparam>
    public interface IEntityTypeOverride<T> where T : class
    {
        /// <summary>
        ///     Alter the mappings for this entity
        /// </summary>
        /// <param name="mapping">EntityTypeBuilder</param>
        void Override(EntityTypeBuilder<T> mapping);
    }
}