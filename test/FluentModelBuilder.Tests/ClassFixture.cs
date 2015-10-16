using System;
using FluentModelBuilder.InMemory;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
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

    public abstract class TestBase : ClassFixture
    {
        protected TestBase(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework().AddDbContext<DbContext>(x =>
            {
                ConfigureOptions(x);
            }).AddInMemoryFluentProvider();
        }

        protected abstract void ConfigureOptions(DbContextOptionsBuilder options);
    }

    public class SingleEntity
    {
        public int Id { get; set; }
        public string StringProperty { get; set; }
        public DateTime DateProperty { get; set; }
    }

    public class OtherSingleEntity
    {
        public int Id { get; set; }
        public string OtherStringProperty { get; set; }
    }

    public class ModelFixture
    {
        public IServiceCollection Services { get; set; } = new ServiceCollection();

        public IModel CreateModel()
        {
            return Services.BuildServiceProvider().GetService<DbContext>().Model;
        }
    }
}