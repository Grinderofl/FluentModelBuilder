using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.Sources.Assemblies
{
    public interface IAssemblySource
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}
