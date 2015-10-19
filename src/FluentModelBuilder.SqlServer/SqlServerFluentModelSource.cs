using FluentModelBuilder.Core;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace FluentModelBuilder.SqlServer
{
    public class SqlServerFluentModelSource : SqlServerModelSource
    {
        private readonly IModelBuilderMutator _mutator;
        public SqlServerFluentModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder, IModelBuilderMutator mutator) : base(setFinder, coreConventionSetBuilder)
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
