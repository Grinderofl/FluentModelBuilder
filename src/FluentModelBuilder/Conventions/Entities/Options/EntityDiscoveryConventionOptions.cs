using System.Collections.Generic;
using FluentModelBuilder.Conventions.Core.Criteria;
using FluentModelBuilder.Options;
using FluentModelBuilder.Sources.Assemblies;

namespace FluentModelBuilder.Conventions.Entities.Options
{
    public class EntityDiscoveryConventionOptions : IAssemblyOptions
    {
        public IList<IAssemblySource> AssemblySources { get; set; } = new List<IAssemblySource>();
        public IList<ITypeInfoCriteria> Criterias { get; set; } = new List<ITypeInfoCriteria>();
    }
}