using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace FluentModelBuilder
{
    public class AutoCoreConventionSetBuilder : CoreConventionSetBuilder
    {
        private readonly FluentModelBuilderConfiguration _configuration;

        public AutoCoreConventionSetBuilder(FluentModelBuilderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override ConventionSet CreateConventionSet()
        {
            var conventionSet = base.CreateConventionSet();

            _configuration.Alterations.Apply(conventionSet);
            return conventionSet;
        }
    }
}