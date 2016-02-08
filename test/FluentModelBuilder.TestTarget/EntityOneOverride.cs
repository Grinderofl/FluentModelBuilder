using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Alterations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.TestTarget
{
    public class EntityOneOverride : IEntityTypeOverride<EntityOne>
    {
        public void Override(EntityTypeBuilder<EntityOne> mapping)
        {
            mapping.Ignore(x => x.IgnoredInOverride);
        }
    }
}
