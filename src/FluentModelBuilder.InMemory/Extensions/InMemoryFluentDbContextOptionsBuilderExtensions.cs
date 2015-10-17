using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.InMemory.Extensions
{
    public static class InMemoryFluentDbContextOptionsBuilderExtensions
    {
        public static FluentDbContextOptionsBuilder WithInMemoryDatabase(this FluentDbContextOptionsBuilder builder)
        {
            builder.OptionsBuilder.UseInMemoryDatabase();
            return builder.WithExtension<InMemoryBuilderExtension>();
        }
    }
}