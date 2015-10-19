using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory;

namespace FluentModelBuilder.MicrosoftSqlServer.Extensions
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