using FluentModelBuilder.Conventions.Core.Options;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions.Core
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