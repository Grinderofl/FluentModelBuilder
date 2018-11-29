using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.Extensions.Accessors
{
    public abstract class FieldPropertyAccessor : PropertyAccessor
    {
        protected FieldPropertyAccessor() : base(PropertyAccessMode.Field)
        {
        }

        public override void Modify<T>(PropertyBuilder<T> entry)
        {
            base.Modify(entry);
            var propertyName = entry.Metadata.Name;
            var fieldName = CreateFieldName(propertyName);
            entry.HasField(fieldName);
        }

        protected abstract string CreateFieldName(string propertyName);
    }
}