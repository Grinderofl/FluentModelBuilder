using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder
{
    public class AssembliesBuilder
    {
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();

        public AssembliesBuilder AddAssembly(Assembly assembly)
        {
            if(!Assemblies.Contains(assembly))
                Assemblies.Add(assembly);
            return this;
        }
    }
}