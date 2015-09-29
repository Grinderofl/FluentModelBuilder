using System.Collections.Generic;
using System.Reflection;

namespace ConventionModelBuilder.Options
{
    public interface IAssemblyOptions
    {
        IList<Assembly> Assemblies { get; }
    }
}