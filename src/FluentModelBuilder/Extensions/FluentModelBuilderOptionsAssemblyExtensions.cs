using FluentModelBuilder.Conventions.Core.Options.Extensions;
using FluentModelBuilder.Options;

namespace FluentModelBuilder.Extensions
{
    public static class FluentModelBuilderOptionsAssemblyExtensions
    {
        public static FluentModelBuilderOptions AddAssemblyContaining<T>(this FluentModelBuilderOptions options)
        {
            return options.Assemblies(assembly => assembly.AddAssemblyContaining<T>());
        }
    }
}