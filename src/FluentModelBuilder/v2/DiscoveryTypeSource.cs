using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Conventions.Core.Criteria;

namespace FluentModelBuilder.v2
{
    public class DiscoveryTypeSource : ITypeSource
    {
        private readonly IList<Assembly> _assemblies;
        private readonly IList<ITypeInfoCriteria> _criterias;

        public DiscoveryTypeSource(IList<Assembly> assemblies, IList<ITypeInfoCriteria> criterias)
        {
            _assemblies = assemblies;
            _criterias = criterias;
        }

        public IEnumerable<Type> GetTypes()
        {
            return _assemblies
                .SelectMany(x => x.GetExportedTypes())
                .Where(x => _criterias.Any(c => c.IsSatisfiedBy(x.GetTypeInfo())));
        }
    }
}