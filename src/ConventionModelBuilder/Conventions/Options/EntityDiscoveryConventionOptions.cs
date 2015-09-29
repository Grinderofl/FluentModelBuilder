using System.Collections.Generic;
using System.Reflection;
using ConventionModelBuilder.Conventions.Criteria;
using ConventionModelBuilder.Extensions;
using ConventionModelBuilder.Options;

namespace ConventionModelBuilder.Conventions.Options
{
    public class EntityDiscoveryConventionOptions : IAssemblyOptions
    {
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
        public IList<ITypeInfoCriteria> Criterias { get; set; } = new List<ITypeInfoCriteria>();
    }
}