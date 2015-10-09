using FluentModelBuilder.Conventions.Assemblies.Options;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions.Assemblies
{

    /// <summary>
    /// Holds assemblies for other conventions to use
    /// </summary>
    public class AssemblyConvention : IModelBuilderConvention
    {
        public AssemblyConventionOptions Options { get; } = new AssemblyConventionOptions();

        public void Apply(ModelBuilder builder)
        {
            // Doesn't actually do anything
        }
    }
}