using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.Options
{
    /// <summary>
    /// Provides point for extension methods in re-usable configuration options
    /// </summary>
    public interface IAssemblyOptions
    {
        IList<Assembly> Assemblies { get; }
    }
}