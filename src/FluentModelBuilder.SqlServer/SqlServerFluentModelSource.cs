using FluentModelBuilder.Contributors;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace FluentModelBuilder.SqlServer
{
    public class SqlServerFluentModelSource : SqlServerModelSource
    {
        private readonly IFluentBuilderContributor _contributor;
        public SqlServerFluentModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder, IFluentBuilderContributor contributor) : base(setFinder, coreConventionSetBuilder)
        {
            _contributor = contributor;
        }

        protected override void FindSets(ModelBuilder modelBuilder, DbContext context)
        {
            base.FindSets(modelBuilder, context);
            _contributor.Contribute(modelBuilder, context);
        }
    }
}
