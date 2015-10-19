using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core
{
    public interface IFluentModelBuilder
    {
        void Apply(ModelBuilder modelBuilder, DbContext dbContext);
    }
}