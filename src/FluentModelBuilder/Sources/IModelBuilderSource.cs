using FluentModelBuilder.Options;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace FluentModelBuilder.Sources
{
    public interface IModelBuilderSource
    {
        ModelBuilder CreateModelBuilder(FluentModelBuilderOptions options, ConventionSet conventionSet, Model model = null);
    }
}