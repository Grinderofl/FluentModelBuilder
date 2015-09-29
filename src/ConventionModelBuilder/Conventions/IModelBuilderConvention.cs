using Microsoft.Data.Entity;

namespace ConventionModelBuilder.Conventions
{
    public interface IModelBuilderConvention
    {
        void Apply(ModelBuilder builder);
    }
}