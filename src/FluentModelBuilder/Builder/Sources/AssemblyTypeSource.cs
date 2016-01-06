using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentModelBuilder.Builder.Sources
{
    public class AssemblyTypeSource : ITypeSource
    {
        private readonly Assembly _assembly;

        public AssemblyTypeSource(Assembly assembly)
        {
            _assembly = assembly;
        }

        public IEnumerable<Type> GetTypes()
        {
            return _assembly.GetTypes().OrderBy(x => x.FullName);
        }

        public string GetIdentifier()
        {
            return _assembly.FullName;
        }
    }
}