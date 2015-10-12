using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.v2
{
    public class AssemblyCoreDescriptorOptions
    {
        public IList<Assembly> Assemblies { get; } = new List<Assembly>();
    }
}