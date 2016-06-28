using System;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace FluentModelBuilder.Configuration
{
    public class AutoCoreConventionSetBuilder : CoreConventionSetBuilder
    {
        private readonly FluentModelBuilderConfiguration _configuration;

        public AutoCoreConventionSetBuilder(FluentModelBuilderConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
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