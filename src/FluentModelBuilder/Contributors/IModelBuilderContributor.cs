using Microsoft.Data.Entity;

namespace FluentModelBuilder
{
    public interface IModelBuilderContributor
    {
        void Contribute(ModelBuilder modelBuilder);
    }
}