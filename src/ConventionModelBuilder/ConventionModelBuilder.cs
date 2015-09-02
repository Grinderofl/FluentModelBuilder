using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.Metadata.Internal;

namespace ConventionModelBuilder
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ConventionModelBuilder
    {
        protected ConventionModelBuilderOptions Options;

        protected ICoreConventionSetBuilder CoreConventionSetBuilder { get; }

        public ConventionModelBuilder(ConventionModelBuilderOptions options)
        {
            Options = options;
        }
        
        public IModel Build()
        {
            var builder = CreateModelBuilder();
            ApplyConventions(builder);
        }

        protected virtual void ApplyConventions(ModelBuilder builder)
        {
            foreach(var convention in Options.ModelBuilderConventions)
                convention.Apply(builder);
        }

        protected virtual ModelBuilder CreateModelBuilder()
        {
            var conventions = 
                ? new CoreConventionSetBuilder().CreateConventionSet()
                : new ConventionSet();
            return new ModelBuilder(conventions);
        }
    }
}
