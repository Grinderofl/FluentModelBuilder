using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions
{
    /// <summary>
    /// Single convention for <see cref="FluentModelBuilder"/>
    /// </summary>
    public interface IModelBuilderConvention
    {
        /// <summary>
        /// Applies convention to <see cref="ModelBuilder"/>
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/></param>
        void Apply(ModelBuilder builder);
    }
}