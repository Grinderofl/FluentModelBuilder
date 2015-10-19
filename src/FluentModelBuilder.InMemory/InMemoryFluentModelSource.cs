using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Core;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure.Internal;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace FluentModelBuilder.InMemory
{
    public class InMemoryFluentModelSource : InMemoryModelSource
    {
        private readonly IFluentModelBuilder _mutator;
        public InMemoryFluentModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder, IFluentModelBuilder mutator) : base(setFinder, coreConventionSetBuilder)
        {
            _mutator = mutator;
        }

        protected override void FindSets(ModelBuilder modelBuilder, DbContext context)
        {
            base.FindSets(modelBuilder, context);
            _mutator.Apply(modelBuilder, context);
        }
    }
}
