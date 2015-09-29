using ConventionModelBuilder.Options;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace ConventionModelBuilder.Sources
{
    public interface IConventionSetSource
    {
        ConventionSet CreateConventionSet(ConventionModelBuilderOptions options);
    }
}