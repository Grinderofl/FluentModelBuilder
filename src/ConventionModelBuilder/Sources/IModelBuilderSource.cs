using ConventionModelBuilder.Options;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace ConventionModelBuilder.Sources
{
    public interface IModelBuilderSource
    {
        ModelBuilder CreateModelBuilder(ConventionModelBuilderOptions options, ConventionSet conventionSet, Model model = null);
    }
}