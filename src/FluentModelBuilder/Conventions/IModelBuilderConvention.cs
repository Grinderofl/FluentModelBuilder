using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Conventions
{
    /// <summary>
    ///     Provides a way to override the configuration of the ModelBuilder
    /// </summary>
    public interface IModelBuilderConvention
    {
        void Apply(ModelBuilder modelBuilder);
    }
}