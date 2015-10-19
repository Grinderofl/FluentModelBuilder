using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core
{
    public interface IModelBuilderMutator
    {
        void Apply(ModelBuilder modelBuilder, DbContext dbContext);
    }
}