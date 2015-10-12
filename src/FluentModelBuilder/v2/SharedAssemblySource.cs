using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.v2
{
    public class SharedAssemblySource : ISharedAssemblySource
    {
        private readonly IList<Assembly> _assemblies;

        public SharedAssemblySource(IList<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            return _assemblies;
        }
    }
}