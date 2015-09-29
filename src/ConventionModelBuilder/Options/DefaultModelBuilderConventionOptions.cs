using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConventionModelBuilder.Options
{
    public class DefaultModelBuilderConventionOptions
    {
        public IEnumerable<Assembly> Assemblies { get; set; } = new List<Assembly>();
        public List<Type> BaseTypes { get; set; } = new List<Type>();
    }
}