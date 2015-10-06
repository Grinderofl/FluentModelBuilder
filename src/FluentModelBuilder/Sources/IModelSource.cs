using FluentModelBuilder.Options;
using Microsoft.Data.Entity.Metadata;

namespace FluentModelBuilder.Sources
{
    public interface IModelSource
    {
        Model CreateModel(FluentModelBuilderOptions options);
    }
}