using ConventionModelBuilder.Options;
using Microsoft.Data.Entity.Metadata;

namespace ConventionModelBuilder.Sources
{
    public class DefaultModelSource : IModelSource
    {
        public Model CreateModel(ConventionModelBuilderOptions options)
        {
            return new Model();
        }
    }
}