using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FluentModelBuilder.Relational.Conventions
{
    public class PluralizingTableNameConvention : AbstractTableNameConvention
    {
        private readonly bool _shouldPluralize;

        public PluralizingTableNameConvention(bool shouldPluralize = true)
        {
            _shouldPluralize = shouldPluralize;
        }

        protected override string CreateName(IMutableEntityType entityType)
            => _shouldPluralize ? Inflector.Inflector.Pluralize(entityType.DisplayName()) : entityType.DisplayName();
    }
}
