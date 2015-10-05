using ConventionModelBuilder.Options;
using Microsoft.Data.Entity.Metadata;

namespace ConventionModelBuilder.Sources
{
    public class DefaultModelSource : IModelSource
    {
        public virtual Model CreateModel(ConventionModelBuilderOptions options)
        {
            return new Model();
        }
    }
}