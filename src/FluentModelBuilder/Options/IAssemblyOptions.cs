using System.Collections.Generic;
using System.Reflection;
using FluentModelBuilder.Sources;
using FluentModelBuilder.Sources.Assemblies;

namespace FluentModelBuilder.Options
{
    /// <summary>
    /// Provides point for extension methods in re-usable configuration options
    /// </summary>
    public interface IAssemblyOptions
    {
        IList<IAssemblySource> AssemblySources { get; }
    }
}