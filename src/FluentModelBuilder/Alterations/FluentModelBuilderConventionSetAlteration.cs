using FluentModelBuilder.Configuration;
using FluentModelBuilder.Conventions;

namespace FluentModelBuilder.Alterations
{
    public class FluentModelBuilderConventionSetAlteration : IConventionSetAlteration
    {
        private readonly FluentModelBuilderConfiguration _configuration;

        public FluentModelBuilderConventionSetAlteration(FluentModelBuilderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Alter(Microsoft.Data.Entity.Metadata.Conventions.ConventionSet conventions)
        {
            conventions.ModelInitializedConventions.Add(new FluentModelBuilderConvention(_configuration));
        }
    }
}
