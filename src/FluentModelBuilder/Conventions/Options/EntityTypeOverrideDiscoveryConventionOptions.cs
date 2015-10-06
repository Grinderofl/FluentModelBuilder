using System.Collections.Generic;
using System.Reflection;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;

namespace FluentModelBuilder.Conventions.Options
{
    public class EntityTypeOverrideDiscoveryConventionOptions : IAssemblyOptions
    {
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
    }
}