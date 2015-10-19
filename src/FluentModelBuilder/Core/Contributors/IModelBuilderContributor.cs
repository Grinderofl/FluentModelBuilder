using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core.Contributors
{
    public interface IModelBuilderContributor
    {
        void Contribute(ModelBuilder modelBuilder);
    }
}