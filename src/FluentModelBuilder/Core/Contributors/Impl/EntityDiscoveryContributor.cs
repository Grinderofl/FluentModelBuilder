using System.Linq;
using System.Reflection;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core.Contributors.Impl
{
    public class EntityDiscoveryContributor : DiscoveryContributorBase<EntityDiscoveryContributor>, IEntityContributor
    {
        protected override void ContributeCore(ModelBuilder modelBuilder)
        {
            var types =
                GetAssemblies()
                .Distinct()
                .SelectMany(x => x.GetExportedTypes())
                .Where(x => Criteria.All(c => c.IsSatisfiedBy(x.GetTypeInfo())));

            foreach (var type in types)
                modelBuilder.Entity(type);
        }
    }
}