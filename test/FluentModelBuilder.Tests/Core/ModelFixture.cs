using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.Tests.Core
{
    public class ModelFixture
    {
        public IServiceCollection Services { get; set; } = new ServiceCollection();

        public IModel CreateModel()
        {
            return Services.BuildServiceProvider().GetService<DbContext>().Model;
        }
    }
}