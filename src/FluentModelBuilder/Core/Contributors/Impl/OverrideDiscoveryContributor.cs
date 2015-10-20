using System.Linq;
using System.Reflection;
using FluentModelBuilder.Core.Extensions;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core.Contributors.Impl
{
    public class OverrideDiscoveryContributor : DiscoveryContributorBase<OverrideDiscoveryContributor>, IOverrideContributor
    {
        protected override void ContributeCore(ModelBuilder modelBuilder)
        {
            var types = GetAssemblies().Distinct().SelectMany(x => x.GetExportedTypes());
            var overrideTypes = types.Where(x => x.ImplementsInterfaceOfType(typeof(IEntityTypeOverride<>)));
            var criteriaTypes = overrideTypes.Where(x => Criteria.All(c => c.IsSatisfiedBy(x.GetTypeInfo())));

            foreach (var type in criteriaTypes)
            {
                var contributor = new SingleTypeOverrideContributor(type);
                contributor.Contribute(modelBuilder);
            }
        }
    }
}