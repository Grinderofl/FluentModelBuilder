using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FluentModelBuilder.Relational.Generators
{
    public class PluralizingTableNameGenerator : ITableNameGenerator
    {
        private readonly bool _shouldPluralize;

        public PluralizingTableNameGenerator(bool shouldPluralize = true)
        {
            _shouldPluralize = shouldPluralize;
        }

        public virtual string CreateName(IEntityType entityType)
            => _shouldPluralize ? entityType.DisplayName().Pluralize() : entityType.DisplayName();
    }
}