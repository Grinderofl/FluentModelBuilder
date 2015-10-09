using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.Conventions.Assemblies.Options
{
    public class AssemblyConventionOptions
    {
        public IList<Assembly> Assemblies { get; } = new List<Assembly>();
    }
}