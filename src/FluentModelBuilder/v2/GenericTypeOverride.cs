using System;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.v2
{
    public class GenericTypeOverride<T> : IEntityTypeOverride<T> where T : class
    {
        private readonly Action<EntityTypeBuilder<T>> _mapping;

        public GenericTypeOverride(Action<EntityTypeBuilder<T>> mapping)
        {
            _mapping = mapping;
        }

        public void Configure(EntityTypeBuilder<T> mapping)
        {
            _mapping(mapping);
        }
    }
}