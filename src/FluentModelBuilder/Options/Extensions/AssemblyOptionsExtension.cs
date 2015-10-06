using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FluentModelBuilder.Options.Extensions
{
    public static class AssemblyOptionsExtension
    {
        /// <summary>
        /// Adds an assembly which contains the specified type
        /// </summary>
        /// <param name="options"><see cref="IAssemblyOptions"/></param>
        /// <param name="type">Type of which assembly to add</param>
        /// <returns><see cref="IAssemblyOptions"/></returns>
        public static IAssemblyOptions FromAssemblyContaining(this IAssemblyOptions options, Type type)
        {
            return options.FromAssembly(type.GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Adds an assembly which contains the specified type
        /// </summary>
        /// <typeparam name="T">Type of which assembly to add</typeparam>
        /// <param name="options"><see cref="IAssemblyOptions"/></param>
        /// <returns></returns>
        public static IAssemblyOptions FromAssemblyContaining<T>(this IAssemblyOptions options)
        {
            return options.FromAssemblyContaining(typeof (T));
        }

        /// <summary>
        /// Adds an assembly
        /// </summary>
        /// <param name="options"><see cref="IAssemblyOptions"/></param>
        /// <param name="assemblies">List of assemblies to add</param>
        /// <returns><see cref="IAssemblyOptions"/></returns>
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
