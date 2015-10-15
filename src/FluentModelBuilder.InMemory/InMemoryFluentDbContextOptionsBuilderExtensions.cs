using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.InMemory
{
    public static class InMemoryFluentDbContextOptionsBuilderExtensions
    {
        public static FluentDbContextOptionsBuilder WithInMemoryDatabase(this FluentDbContextOptionsBuilder builder)
        {
            return builder.ModelSource<InMemoryBuilderExtension>();
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