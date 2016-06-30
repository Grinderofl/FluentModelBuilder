using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Alterations
{
    /// <summary>
    ///     Provides a way to override the configuration of the ModelBuilder
    /// </summary>
    public interface IModelBuilderOverride
    {
        void Override(ModelBuilder modelBuilder);
    }
}