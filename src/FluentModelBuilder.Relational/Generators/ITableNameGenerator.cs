using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FluentModelBuilder.Relational.Generators
{
    public interface ITableNameGenerator
    {
        string CreateName(IEntityType entityType);
    }
}
