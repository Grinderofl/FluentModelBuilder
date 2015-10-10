using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Options;
using FluentModelBuilder.Sources;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.SqlServer;

namespace FluentModelBuilder.SqlServer
{
    public class SqlServerConventionSetSource : DefaultConventionSetSource
    {
        private static readonly IConventionSetBuilder ConventionSetBuilder = new SqlServerConventionSetBuilder();
        public SqlServerConventionSetSource(bool useCoreConventions = true) : base(useCoreConventions)
        {
        }

        public override ConventionSet CreateConventionSet(FluentModelBuilderOptions options)
        {
            var baseConventions = base.CreateConventionSet(options);
            return ConventionSetBuilder.AddConventions(baseConventions);
        }
    }

    public class FluentSqlServerModelSource : SqlServerModelSource
    {
        public FluentSqlServerModelSource(IDbSetFinder setFinder, ICoreConventionSetBuilder coreConventionSetBuilder) : base(setFinder, coreConventionSetBuilder)
        {
        }
    }
}
