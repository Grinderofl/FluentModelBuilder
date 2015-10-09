using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.Sources.Assemblies
{
    public class TypeContainingAssemblySource : IAssemblySource
    {
        private readonly Type _type;

        public TypeContainingAssemblySource(Type type)
        {
            _type = type;
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            yield return _type.GetTypeInfo().Assembly;
        }
    }
}