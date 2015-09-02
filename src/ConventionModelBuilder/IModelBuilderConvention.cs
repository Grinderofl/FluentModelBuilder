using Microsoft.Data.Entity;

namespace ConventionModelBuilder
{
    public interface IModelBuilderConvention
    {
        void Apply(ModelBuilder modelBuilder);
    }
}