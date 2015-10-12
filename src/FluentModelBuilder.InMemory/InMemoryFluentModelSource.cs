using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.InMemory;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using FluentModelBuilder.v2;

namespace FluentModelBuilder.InMemory
{
    public class InMemoryFluentModelSource : InMemoryModelSource
    {
        private readonly IModelBuilderApplier _applier;
        public InMemoryFluentModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder, IModelBuilderApplier applier) : base(setFinder, coreConventionSetBuilder)
        {
            _applier = applier;
        }

        protected override void FindSets(ModelBuilder modelBuilder, DbContext context)
        {
            base.FindSets(modelBuilder, context);
            _applier.Apply(modelBuilder);
        }
    }

    public class InMemoryModelSourceApplier : ModelSourceApplierBase
    {
        protected override Type ServiceType => typeof (InMemoryModelSource);
        protected override Type ImplementationType => typeof (InMemoryFluentModelSource);
    }

    public static class Extensions
    {
        public static v2.FluentModelBuilder UsingInMemory(this v2.FluentModelBuilder fmb)
        {
            fmb.UsingModelSource<InMemoryModelSourceApplier>();
            return fmb;
        }
    }
}
