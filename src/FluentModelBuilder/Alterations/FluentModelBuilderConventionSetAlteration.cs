using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace FluentModelBuilder
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
            conventions.ModelInitializedConventions.Add(new FluentModelBuilderConvention(_configuration));
        }
    }
}
