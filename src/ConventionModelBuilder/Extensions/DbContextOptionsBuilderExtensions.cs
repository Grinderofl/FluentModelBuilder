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
        /// <summary>
        /// Replaces the model with one built using provided set of conventions
        /// </summary>
        /// <param name="builder"><see cref="DbContextOptionsBuilder"/></param>
        /// <param name="optionsAction">Configure <see cref="ConventionModelBuilderOptions"/></param>
        /// <returns>Extension of type <see cref="ConventionModelBuilderExtension"/></returns>
        public static ConventionModelBuilderExtension BuildModelUsingConventions(this DbContextOptionsBuilder builder, Action<ConventionModelBuilderOptions> optionsAction = null)
        {
            var options = new ConventionModelBuilderOptions();
            optionsAction?.Invoke(options);
            return new ConventionModelBuilderExtension(builder, options);
        }
    }
}
