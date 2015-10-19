using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors
{
    public interface IFluentBuilderContributor
    {
        void Contribute(ModelBuilder modelBuilder, DbContext dbContext);
    }
}