using ConventionModelBuilder.Options;
using Microsoft.Data.Entity;

namespace ConventionModelBuilder.Sources
{
    /// <summary>
    /// Provides strategy for applying conventions to <see cref="ModelBuilder"/>
    /// </summary>
    public interface IConventionApplier
    {
        void Apply(ModelBuilder modelBuilder, ConventionModelBuilderOptions options);
    }
}