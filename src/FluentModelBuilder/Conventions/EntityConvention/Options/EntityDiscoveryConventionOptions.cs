using System.Collections.Generic;
using FluentModelBuilder.Conventions.Criteria;
using FluentModelBuilder.Options;
using FluentModelBuilder.Sources;
using FluentModelBuilder.Sources.Assemblies;

namespace FluentModelBuilder.Conventions.EntityConvention.Options
{
    public class EntityDiscoveryConventionOptions : IAssemblyOptions
    {
        public IList<IAssemblySource> AssemblySources { get; set; } = new List<IAssemblySource>();
        public IList<ITypeInfoCriteria> Criterias { get; set; } = new List<ITypeInfoCriteria>();
    }
}