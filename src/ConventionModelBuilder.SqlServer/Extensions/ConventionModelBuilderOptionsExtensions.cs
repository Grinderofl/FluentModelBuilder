using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConventionModelBuilder.Options;

namespace ConventionModelBuilder.SqlServer.Extensions
{
    public static class ConventionModelBuilderOptionsExtensions
    {
        public static ConventionModelBuilderOptions UseSqlServer(this ConventionModelBuilderOptions options, bool useCoreConventions = true)
        {
            options.ConventionSetSource = new SqlServerConventionSetSource(useCoreConventions);
            return options;
        }
    }
}
