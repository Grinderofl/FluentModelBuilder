using FluentModelBuilder.Options;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Sources
{
    /// <summary>
    /// Provides strategy for applying conventions to <see cref="ModelBuilder"/>
    /// </summary>
    public interface IConventionApplier
    {
        void Apply(ModelBuilder modelBuilder, FluentModelBuilderOptions options);
    }
}