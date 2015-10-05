using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using ConventionModelBuilder.Options;
using ConventionModelBuilder.Sources;
using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.Sqlite;

namespace ConventionModelBuilder.Sqlite
{
    public class SqliteConventionSetSource : DefaultConventionSetSource
    {
        private static readonly IConventionSetBuilder ConventionSetBuilder = new SqliteConventionSetBuilder();

        public SqliteConventionSetSource(bool useCoreConventions = true) : base(useCoreConventions)
        {
        }

        public override ConventionSet CreateConventionSet(ConventionModelBuilderOptions options)
        {
            var baseConventions = base.CreateConventionSet(options);
            return ConventionSetBuilder.AddConventions(baseConventions);
        }
    }
}
