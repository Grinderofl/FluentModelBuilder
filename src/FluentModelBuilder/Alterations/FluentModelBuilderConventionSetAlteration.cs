using FluentModelBuilder.Builder;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace FluentModelBuilder.Alterations
{
    public class FluentModelBuilderConventionSetAlteration : IConventionSetAlteration
    {
        private readonly FluentModelBuilderConfiguration _configuration;

        public FluentModelBuilderConventionSetAlteration(FluentModelBuilderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Alter(ConventionSet conventions)
        {
            conventions.ModelInitializedConventions.Add(new FluentModelBuilderConvention(_configuration, BuilderScope.Early));
            conventions.ModelBuiltConventions.Add(new FluentModelBuilderConvention(_configuration, BuilderScope.Late));
        }
    }
}
