using System.Linq;
using System.Reflection;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors.Internal
{
    public class DiscoveryOverrideContributor : DiscoveryContributorBase<DiscoveryOverrideContributor>, IOverrideContributor
    {
        public DiscoveryOverrideContributor(AssembliesBuilder builder) : base(builder)
        {
        }

        public DiscoveryOverrideContributor()
        {
        }

        public override void Contribute(ModelBuilder modelBuilder)
        {
            var types = GetAssemblies().Distinct().SelectMany(x => x.GetExportedTypes());
            var overrideTypes = types.Where(x => x.ImplementsInterfaceOfType(typeof(IEntityTypeOverride<>)));
            var criteriaTypes = overrideTypes.Where(x => Criteria.All(c => c.IsSatisfiedBy(x.GetTypeInfo())));

            //var types =
            //    GetAssemblies()
            //        .Distinct()
            //        .SelectMany(x => x.GetExportedTypes())
            //        .Where(x => typeof(IEntityTypeOverride<>).IsAssignableFrom(x))
            //        .Where(x => Criteria.All(c => c.IsSatisfiedBy(x.GetTypeInfo())));

            foreach (var type in criteriaTypes)
            {
                var contributor = new SingleOverrideContributor(type);
                contributor.Contribute(modelBuilder);
            }
        }
    }
}