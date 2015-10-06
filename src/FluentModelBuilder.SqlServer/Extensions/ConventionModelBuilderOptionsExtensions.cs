using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Options;

namespace FluentModelBuilder.SqlServer.Extensions
{
    public static class FluentModelBuilderOptionsExtensions
    {
        public static FluentModelBuilderOptions UseSqlServer(this FluentModelBuilderOptions options, bool useCoreConventions = true)
        {
            options.ConventionSetSource = new SqlServerConventionSetSource(useCoreConventions);
            return options;
        }
    }
}
