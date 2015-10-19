using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.InMemory.Extensions
{
    public static class InMemoryFluentDbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures the DbContext to use In Memory Database with FluentModelBuilder
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder WithInMemoryDatabase(this FluentDbContextOptionsBuilder builder)
        {
            builder.OptionsBuilder.UseInMemoryDatabase();
            return builder.WithExtension<InMemoryBuilderExtension>();
        }
    }
}