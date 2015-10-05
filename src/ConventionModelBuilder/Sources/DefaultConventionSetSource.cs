using ConventionModelBuilder.Options;
using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace ConventionModelBuilder.Sources
{
    public class DefaultConventionSetSource : IConventionSetSource
    {
        private readonly bool _useCoreConventions;
        private static readonly ICoreConventionSetBuilder CoreConventionSetBuilder = new CoreConventionSetBuilder();

        public DefaultConventionSetSource(bool useCoreConventions = true)
        {
            _useCoreConventions = useCoreConventions;
        }

        public virtual ConventionSet CreateConventionSet(ConventionModelBuilderOptions options)
        {
            return _useCoreConventions ? CoreConventionSetBuilder.CreateConventionSet() : new ConventionSet();
        }
    }
}