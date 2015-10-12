using System.Collections.Generic;
using System.Reflection;
using FluentModelBuilder.Conventions.Core.Criteria;

namespace FluentModelBuilder.v2
{
    public class DiscoveryOptions
    {
        public IList<ITypeInfoCriteria> Criterias { get; } = new List<ITypeInfoCriteria>();
        public IList<Assembly> Assemblies { get; } = new List<Assembly>();

        public bool UseSharedAssemblies { get; set; }
    }
}