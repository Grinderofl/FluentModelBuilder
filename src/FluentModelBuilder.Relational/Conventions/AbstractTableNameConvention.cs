using FluentModelBuilder.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FluentModelBuilder.Relational.Conventions
{
    public abstract class AbstractTableNameConvention : AbstractEntityConvention
    {
        protected override void Override(IMutableEntityType entityType)
        {
            entityType.Relational().TableName = CreateName(entityType);
        }

        protected abstract string CreateName(IMutableEntityType entityType);
    }
}