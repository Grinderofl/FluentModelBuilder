using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Options;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Replaces the model with one built using provided set of conventions
        /// </summary>
        /// <param name="builder"><see cref="DbContextOptionsBuilder"/></param>
        /// <param name="optionsAction">Configure <see cref="FluentModelBuilderOptions"/></param>
        /// <returns>Extension of type <see cref="FluentModelBuilderExtension"/></returns>
        public static FluentModelBuilderExtension BuildModel(this DbContextOptionsBuilder builder, Action<FluentModelBuilderOptions> optionsAction = null)
        {
            var options = new FluentModelBuilderOptions();
            optionsAction?.Invoke(options);
            return new FluentModelBuilderExtension(builder, options);
        }
    }
}
