using ConventionModelBuilder.Options;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace ConventionModelBuilder.Sources
{
    public class DefaultModelBuilderSource : IModelBuilderSource
    {
        public virtual ModelBuilder CreateModelBuilder(ConventionModelBuilderOptions options, ConventionSet conventionSet, Model model = null)
        {
            return model != null
                ? new ModelBuilder(conventionSet, model)
                : new ModelBuilder(conventionSet);
        }
    }
}