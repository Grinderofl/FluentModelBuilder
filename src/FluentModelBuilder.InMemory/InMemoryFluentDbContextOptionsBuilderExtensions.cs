using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.InMemory
{
    public static class InMemoryFluentDbContextOptionsBuilderExtensions
    {
        public static FluentDbContextOptionsBuilder WithInMemoryDatabase(this FluentDbContextOptionsBuilder builder)
        {
            builder.OptionsBuilder.UseInMemoryDatabase();
            return builder.WithExtension<InMemoryBuilderExtension>();
        }
    }

    public static class InMemoryFluentEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddInMemoryFluentProvider(this EntityFrameworkServicesBuilder builder)
        {
            return builder.AddModelSourceProvider<InMemoryModelSourceProvider>();
        }
    }
}