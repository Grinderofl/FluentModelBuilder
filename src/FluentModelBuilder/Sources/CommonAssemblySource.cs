using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using FluentModelBuilder.Conventions;
using FluentModelBuilder.Conventions.Core;
using FluentModelBuilder.Options;

namespace FluentModelBuilder.Sources
{
    public class CommonAssemblySource : IAssemblySource
    {
        private readonly FluentModelBuilderOptions _options;

        public CommonAssemblySource(FluentModelBuilderOptions options)
        {
            _options = options;
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            var convention = _options.Conventions.FirstOrDefault(x => x is AssemblyConvention) as AssemblyConvention;
            if (convention == null)
                throw new InstanceNotFoundException("CommonAssemblyConvention was not found.");

            return convention.Options.Assemblies;
        }
    }
}