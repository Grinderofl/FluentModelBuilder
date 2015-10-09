using FluentModelBuilder.Conventions.Assemblies.Options.Extensions;
using FluentModelBuilder.Options;

namespace FluentModelBuilder.Extensions
{
    public static class FluentModelBuilderOptionsAssemblyExtensions
    {
        /// <summary>
        /// Adds an assembly to common assembly convention
        /// </summary>
        /// <typeparam name="T">Type that is contained in requested assembly</typeparam>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddAssemblyContaining<T>(this FluentModelBuilderOptions options)
        {
            return options.Assemblies(assembly => assembly.AddAssemblyContaining<T>());
        }
    }
}