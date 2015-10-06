using FluentModelBuilder.Options;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace FluentModelBuilder.Sources
{
    public class DefaultModelBuilderSource : IModelBuilderSource
    {
        public virtual ModelBuilder CreateModelBuilder(FluentModelBuilderOptions options, ConventionSet conventionSet, Model model = null)
        {
            return model != null
                ? new ModelBuilder(conventionSet, model)
                : new ModelBuilder(conventionSet);
        }
    }
}