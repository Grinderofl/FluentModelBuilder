using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Contributors;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure.Internal;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace FluentModelBuilder.InMemory
{
    public class InMemoryFluentModelSource : InMemoryModelSource
    {
        private readonly IFluentBuilderContributor _contributor;
        public InMemoryFluentModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder, IFluentBuilderContributor contributor) : base(setFinder, coreConventionSetBuilder)
        {
            _contributor = contributor;
        }

        protected override void FindSets(ModelBuilder modelBuilder, DbContext context)
        {
            base.FindSets(modelBuilder, context);
            _contributor.Contribute(modelBuilder, context);
        }
    }
}
