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
            conventionSet.ModelInitializedConventions.Add(new FluentModelBuilderConvention(_configuration));
            //_configuration.ConventionSetAlterations.Add(new FluentModelBuilderConventionSetAlteration(_configuration));
            //foreach (var alteration in _configuration.ConventionSetAlterations)
            //    alteration.Alter(conventionSet);
            return conventionSet;
        }
    }
}