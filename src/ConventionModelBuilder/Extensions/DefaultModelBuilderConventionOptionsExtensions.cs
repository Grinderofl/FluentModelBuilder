using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConventionModelBuilder.Conventions;
using ConventionModelBuilder.Options;

namespace ConventionModelBuilder.Extensions
{
    public static class DefaultModelBuilderConventionOptionsExtensions
    {
        public static void FromAssemblies(this DefaultModelBuilderConventionOptions options,
            IEnumerable<Assembly> assemblies)
        {
            options.Assemblies = options.Assemblies.Union(assemblies).Distinct();
        }
    }
}