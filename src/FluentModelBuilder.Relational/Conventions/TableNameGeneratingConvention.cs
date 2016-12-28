using System;
using FluentModelBuilder.Conventions;
using FluentModelBuilder.Relational.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.Relational.Conventions
{
    public class TableNameGeneratingConvention : AbstractEntityConvention
    {
        private readonly ITableNameGenerator _generator;

        public TableNameGeneratingConvention(ITableNameGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            _generator = generator;
        }

        protected override void Apply(EntityTypeBuilder entityType)
        {
            entityType.ToTable(_generator.CreateName(entityType.Metadata.RootType()));
        }
    }
}