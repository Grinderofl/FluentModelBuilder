using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConventionModelBuilder.Options;
using Microsoft.Data.Entity;

namespace ConventionModelBuilder.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static ConventionModelBuilderExtension BuildModelUsingConventions(this DbContextOptionsBuilder builder, Action<ConventionModelBuilderOptions> optionsAction = null)
        {
            var options = new ConventionModelBuilderOptions();
            optionsAction?.Invoke(options);
            return new ConventionModelBuilderExtension(builder, options);
        }
    }
}
