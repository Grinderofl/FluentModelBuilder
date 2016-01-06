using FluentModelBuilder.Configuration;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace FluentModelBuilder.ConventionSet
{
    public class AutoCoreConventionSetBuilder : CoreConventionSetBuilder
    {
        private readonly FluentModelBuilderConfiguration _configuration;

        public AutoCoreConventionSetBuilder(FluentModelBuilderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override Microsoft.Data.Entity.Metadata.Conventions.ConventionSet CreateConventionSet()
        {
            var conventionSet = base.CreateConventionSet();

            _configuration.Alterations.Apply(conventionSet);
            return conventionSet;
        }
    }
}