using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentModelBuilder.Sources;

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
        public static T FromAssemblyContaining<T>(this T options, Type type)
        {
            return options.FromAssembly(type.GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Adds an assembly which contains the specified type
        /// </summary>
        /// <typeparam name="T">Type of which assembly to add</typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="options"><see cref="IAssemblyOptions"/></param>
        /// <returns></returns>
        public static T FromAssemblyContaining<T, T2>(this T options) where T : IAssemblyOptions
        {
            return options.FromAssemblyContaining(typeof (T2));
        }

        /// <summary>
        /// Adds one or more single assemblies
        /// </summary>
        /// <param name="options"><see cref="IAssemblyOptions"/></param>
        /// <param name="assemblies">List of assemblies to add</param>
        /// <returns><see cref="IAssemblyOptions"/></returns>
        public static T FromAssembly<T>(this T options, params Assembly[] assemblies) where T : IAssemblyOptions
        {
            foreach (var assembly in assemblies)
            {
                var assemblySource =
                    options.AssemblySources.FirstOrDefault(x => x is ListAssemblySource) as ListAssemblySource;
                if (assemblySource == null)
                {
                    assemblySource = new ListAssemblySource();
                    options.AssemblySources.Add(assemblySource);
                }
                if(!assemblySource.Assemblies.Contains(assembly))
                    assemblySource.Assemblies.Add(assembly);
            }
            return options;
        }

        public static T FromAssemblySource<T>(this T options, IAssemblySource source) where T : IAssemblyOptions
        {
            if (!options.AssemblySources.Contains(source))
                options.AssemblySources.Add(source);
            return options;
        }

        public static T FromAssemblyConvention<T>(this T options, FluentModelBuilderOptions fmbOptions) where T : IAssemblyOptions
        {
            if(options.AssemblySources.All(ContainsCommonAssemblySource))
                options.AssemblySources.Add(new CommonAssemblySource(fmbOptions));
            return options;
        }

        private static bool ContainsCommonAssemblySource(IAssemblySource x)
        {
            return x.GetType() != typeof (CommonAssemblySource);
        }
    }
}
