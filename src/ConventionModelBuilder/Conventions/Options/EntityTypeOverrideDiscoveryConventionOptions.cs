using System.Collections.Generic;
using System.Reflection;
using ConventionModelBuilder.Extensions;
using ConventionModelBuilder.Options;

namespace ConventionModelBuilder.Conventions.Options
{
    public class EntityTypeOverrideDiscoveryConventionOptions : IAssemblyOptions
    {
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
    }
}