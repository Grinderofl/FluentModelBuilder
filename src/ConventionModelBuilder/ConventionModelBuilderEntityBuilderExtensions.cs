using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace ConventionModelBuilder
{
    public static class ConventionModelBuilderEntityBuilderExtensions
    {
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static void UseConventionModel(this DbContextOptionsBuilder builder, Action<ConventionModelBuilderOptions> optionsAction = null)
        {
            var options = new ConventionModelBuilderOptions();
            optionsAction?.Invoke(options);
            var modelBuilder = new ConventionModelBuilder(options);
            builder.UseModel(modelBuilder.Build());
        }
    }
}
