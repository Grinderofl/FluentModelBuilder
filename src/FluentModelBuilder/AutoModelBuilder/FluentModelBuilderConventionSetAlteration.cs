using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace FluentModelBuilder
{
    //public class FluentModelBuilderCoreConventionSetBuilder : CoreConventionSetBuilder
    //{
    //    private FluentModelBuilderConfiguration _fluentModelBuilder;

    //    public FluentModelBuilderCoreConventionSetBuilder(FluentModelBuilderConfiguration fluentModelBuilder)
    //    {
    //        _fluentModelBuilder = fluentModelBuilder;
    //    }

    //    public override ConventionSet CreateConventionSet()
    //    {
    //        var set = base.CreateConventionSet();

    //        set.ModelInitializedConventions.Add(new FluentModelBuilderConvention(_fluentModelBuilder));
    //        return set;
    //    }
    //}

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
