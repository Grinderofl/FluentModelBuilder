using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace FluentModelBuilder.Sources
{
    public interface IAssemblySource
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}
