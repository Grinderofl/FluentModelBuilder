using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.Sources
{
    public class ListAssemblySource : IAssemblySource
    {
        public IList<Assembly> Assemblies { get; } = new List<Assembly>();

        public IEnumerable<Assembly> GetAssemblies()
        {
            return Assemblies;
        }
    }
}