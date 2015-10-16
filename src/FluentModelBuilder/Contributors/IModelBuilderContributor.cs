using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors
{
    public interface IModelBuilderContributor
    {
        void Contribute(ModelBuilder modelBuilder);
    }
}