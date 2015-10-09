using System.Reflection;

namespace FluentModelBuilder.Conventions.Assemblies.Options.Extensions
{
    public static class AssemblyConventionOptionsExtensions
    {
        /// <summary>
        /// Adds an assembly that contains the specified type to discovery process
        /// </summary>
        /// <typeparam name="T">Type of entity contained in requested assembly</typeparam>
        /// <param name="options"><see cref="AssemblyConventionOptions"/></param>
        /// <returns><see cref="AssemblyConventionOptions"/></returns>
        public static AssemblyConventionOptions AddAssemblyContaining<T>(this AssemblyConventionOptions options)
        {
            var assembly = typeof (T).GetTypeInfo().Assembly;
            return options.AddAssembly(assembly);
        }

        /// <summary>
        /// Adds an assembly to discovery process
        /// </summary>
        /// <param name="options"><see cref="AssemblyConventionOptions"/></param>
        /// <param name="assembly">Assembly to add</param>
        /// <returns><see cref="AssemblyConventionOptions"/></returns>
        public static AssemblyConventionOptions AddAssembly(this AssemblyConventionOptions options, Assembly assembly)
        {
            if (!options.Assemblies.Contains(assembly))
                options.Assemblies.Add(assembly);

            return options;
        }
    }
}