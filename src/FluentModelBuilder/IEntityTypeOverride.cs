using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder
{
    /// <summary>
    /// Overrides a single entity type mapping configuration
    /// </summary>
    /// <typeparam name="T">Type of entity to configure</typeparam>
    public interface IEntityTypeOverride<T> where T : class
    {
        /// <summary>
        /// Configures single entity in model builder
        /// </summary>
        /// <param name="mapping"><see cref="EntityTypeBuilder"/></param>
        void Configure(EntityTypeBuilder<T> mapping);
    }
}