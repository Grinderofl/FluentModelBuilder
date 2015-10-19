using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Contributors.Internal.Criteria;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors.Internal
{
    public class DiscoveryEntityContributor : DiscoveryContributorBase<DiscoveryEntityContributor>, IEntityContributor
    {
        public DiscoveryEntityContributor(AssembliesBuilder builder)
        {
            AssembliesBuilder = builder;
        }

        public DiscoveryEntityContributor()
        {}

        public override void Contribute(ModelBuilder modelBuilder)
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