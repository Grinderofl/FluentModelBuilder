using Microsoft.EntityFrameworkCore.Metadata;

namespace FluentModelBuilder.Relational.Generators
{
    public interface ITableNameGenerator
    {
        string CreateName(IEntityType entityType);
    }
}
