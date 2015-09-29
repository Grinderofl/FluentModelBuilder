using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConventionModelBuilder.Options.Extensions
{
    public static class AssemblyOptionsExtension
    {
        public static IAssemblyOptions FromAssemblyContaining(this IAssemblyOptions options, Type type)
        {
            return options.FromAssembly(type.GetTypeInfo().Assembly);
        }

        public static IAssemblyOptions FromAssemblyContaining<T>(this IAssemblyOptions options)
        {
            return options.FromAssemblyContaining(typeof (T));
        }

        public static IAssemblyOptions FromAssembly(this IAssemblyOptions options, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                if(!options.Assemblies.Contains(assembly))
                    options.Assemblies.Add(assembly);
            }
            return options;
        }
    }
}
