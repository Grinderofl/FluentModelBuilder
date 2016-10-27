using System.Linq;
using FluentModelBuilder.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.Relational.Conventions
{
    /// <summary>
    ///     Specifies the default length for decimal properties
    /// </summary>
    public class DecimalPropertyConvention : AbstractEntityConvention
    {
        private readonly byte _precision;
        private readonly byte _scale;

        public DecimalPropertyConvention() : this(18, 2)
        {
        }

        public DecimalPropertyConvention(byte precision, byte scale)
        {
            _precision = precision;
            _scale = scale;
        }

        protected override void Apply(EntityTypeBuilder entityType)
        {
            foreach (var property in entityType.Metadata.GetProperties().Where(x => x.ClrType == typeof(decimal)))
            {
                property.Relational().ColumnType = $"DECIMAL({_precision},{_scale})";
            }
        }
    }
}
