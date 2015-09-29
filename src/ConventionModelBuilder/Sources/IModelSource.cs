using ConventionModelBuilder.Options;
using Microsoft.Data.Entity.Metadata;

namespace ConventionModelBuilder.Sources
{
    public interface IModelSource
    {
        Model CreateModel(ConventionModelBuilderOptions options);
    }
}