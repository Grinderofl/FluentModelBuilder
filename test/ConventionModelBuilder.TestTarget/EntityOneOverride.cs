using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConventionModelBuilder.Conventions.Overrides;
using Microsoft.Data.Entity.Metadata.Builders;

namespace ConventionModelBuilder.TestTarget
{
    public class EntityOneOverride : IEntityTypeOverride<EntityOne>
    {
        public void Configure(EntityTypeBuilder<EntityOne> mapping)
        {
            mapping.Ignore(x => x.IgnoredInOverride);
        }
    }
}
