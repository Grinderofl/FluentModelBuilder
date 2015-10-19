using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Contributors;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure.Internal;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace FluentModelBuilder.Sqlite
{
    public class SqliteFluentModelSource : SqliteModelSource
    {
        private readonly IModelBuilderMutator _mutator;
        public SqliteFluentModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder, IModelBuilderMutator mutator) : base(setFinder, coreConventionSetBuilder)
        {
            _mutator = mutator;
        }

        protected override void FindSets(ModelBuilder modelBuilder, DbContext context)
        {
            base.FindSets(modelBuilder, context);
            _mutator.Apply(modelBuilder, context);
        }
    }
}
