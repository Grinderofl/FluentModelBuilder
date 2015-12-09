using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.Extensions
{
    public static class AssembliesBuilderExtensions
    {
        public static AssembliesBuilder Add(this AssembliesBuilder builder, IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
                builder.AddAssembly(assembly);
            return builder;
        }
    }
}