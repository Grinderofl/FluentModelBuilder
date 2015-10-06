using System.Collections.Generic;
using System.Reflection;
using FluentModelBuilder.Conventions.Criteria;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;

namespace FluentModelBuilder.Conventions.Options
{
    public class EntityDiscoveryConventionOptions : IAssemblyOptions
    {
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
        public IList<ITypeInfoCriteria> Criterias { get; set; } = new List<ITypeInfoCriteria>();
    }
}