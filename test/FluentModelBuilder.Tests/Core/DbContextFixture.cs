using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.Tests
{
    public class DbContextFixture<T> where T : DbContext
    {
        public IServiceCollection Services;

        public DbContextFixture()
        {
            Services = new ServiceCollection();
        }

        public IModel CreateModel()
        {
            return
                Services.BuildServiceProvider()
                    .GetService<T>()
                    .Model;
        }
    }
}