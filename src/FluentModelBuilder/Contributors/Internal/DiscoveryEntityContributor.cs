using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Entity;

namespace FluentModelBuilder
{
    public class DiscoveryEntityContributor : IEntityContributor
    {
        public IList<Assembly> Assemblies { get; set; }
        public IList<ITypeInfoCriterion> Criteria { get; set; }

        public DiscoveryEntityContributor AddAssembly(Assembly assembly)
        {
            if(!Assemblies.Contains(assembly))
                Assemblies.Add(assembly);
            return this;
        }

        public DiscoveryEntityContributor AddCriterion(ITypeInfoCriterion criterion)
        {
            if(!Criteria.Contains(criterion))
                Criteria.Add(criterion);

            return this;
        }

        public void Contribute(ModelBuilder modelBuilder)
        {
            var types =
                Assemblies.SelectMany(x => x.GetExportedTypes())
                    .Where(x => Criteria.Any(c => c.IsSatisfiedBy(x.GetTypeInfo())));

            foreach (var type in types)
                modelBuilder.Entity(type);
        }
    }
}