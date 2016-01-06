using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentModelBuilder
{
    public class CombinedAssemblyTypeSource : ITypeSource
    {
        private readonly IEnumerable<AssemblyTypeSource> _sources;

        public CombinedAssemblyTypeSource(IEnumerable<Assembly> sources) : this(sources.Select(x => new AssemblyTypeSource(x)))
        {
        }

        public CombinedAssemblyTypeSource(IEnumerable<AssemblyTypeSource> sources)
        {
            _sources = sources;
        }

        public IEnumerable<Type> GetTypes()
        {
            return _sources.SelectMany(x => x.GetTypes()).ToArray();
        }

        public string GetIdentifier()
        {
            return "Combined source";
        }
    }
}