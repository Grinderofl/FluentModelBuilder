using FluentModelBuilder.Options;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace FluentModelBuilder.Sources
{
    public interface IConventionSetSource
    {
        ConventionSet CreateConventionSet(FluentModelBuilderOptions options);
    }
}