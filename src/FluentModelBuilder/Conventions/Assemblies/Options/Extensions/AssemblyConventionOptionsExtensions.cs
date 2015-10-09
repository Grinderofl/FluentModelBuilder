using System.Reflection;

namespace FluentModelBuilder.Conventions.Core.Options.Extensions
{
    public static class AssemblyConventionOptionsExtensions
    {
        public static AssemblyConventionOptions AddAssemblyContaining<T>(this AssemblyConventionOptions options)
        {
            var assembly = typeof (T).GetTypeInfo().Assembly;
            return options.AddAssembly(assembly);
        }

        public static AssemblyConventionOptions AddAssembly(this AssemblyConventionOptions options, Assembly assembly)
        {
            if (!options.Assemblies.Contains(assembly))
                options.Assemblies.Add(assembly);

            return options;
        }
    }
}