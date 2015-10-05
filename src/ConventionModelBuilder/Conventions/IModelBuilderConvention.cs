using Microsoft.Data.Entity;

namespace ConventionModelBuilder.Conventions
{
    /// <summary>
    /// Single convention for <see cref="ConventionModelBuilder"/>
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