using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors
{
    public interface IModelBuilderMutator
    {
        void Apply(ModelBuilder modelBuilder, DbContext dbContext);
    }
}