using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.Options
{
    public class DefaultModelBuilderConventionOptions : IAssemblyOptions
    {
        /// <summary>
        /// List of Assemblies to search entities from
        /// </summary>
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();

        /// <summary>
        /// List of base types to search entities by
        /// </summary>
        public List<Type> BaseTypes { get; set; } = new List<Type>();
    }
}