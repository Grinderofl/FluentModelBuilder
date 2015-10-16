using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class ClassFixture : IClassFixture<ModelFixture>
    {
        protected IModel Model;

        protected ClassFixture(ModelFixture fixture)
        {
            Configure(fixture.Services);
            Model = fixture.CreateModel();
        }

        private void Configure(IServiceCollection services)
        {
            ConfigureServices(services);
        }
        protected abstract void ConfigureServices(IServiceCollection services);
    }
}