using System;
using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.SqlServer.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingAndConfiguringSingleEntityToModelInOnConfiguringInCustomDbContext : IClassFixture<DbContextFixture<AddingAndConfiguringSingleEntityToModelInOnConfiguringInCustomDbContext.TestContext>>
    {
        protected IModel Model;

        public AddingAndConfiguringSingleEntityToModelInOnConfiguringInCustomDbContext(DbContextFixture<TestContext> fixture)
        {
            ConfigureServices(fixture.Services);
            Model = fixture.CreateModel();
        }

        public class TestContext : DbContext
        {
            public TestContext(IServiceProvider serviceProvider) : base(serviceProvider)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                options.ConfigureModel()
                    .Entities(e => e.Add<SingleEntity>(c => c.Property<long>("CustomProperty")))
                    .WithInMemoryDatabase();
            }
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework().AddDbContext<TestContext>().AddInMemoryFluentModelBuilder();
        }

        [Fact]
        public void AddsSingleEntity()
        {
            Assert.Equal(1, Model.GetEntityTypes().OrderBy(x => x.Name).Count());
            Assert.Equal(typeof(SingleEntity), Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(0).ClrType);
        }

        [Fact]
        public void MapsProperties()
        {
            var properties = Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(0).GetProperties().OrderBy(x => x.Name).ToArray();
            Assert.Equal("Id", properties[2].Name);
            Assert.Equal("CustomProperty", properties[0].Name);
            Assert.Equal("DateProperty", properties[1].Name);
            Assert.Equal("StringProperty", properties[3].Name);

            Assert.Equal(typeof(int), properties[2].ClrType);
            Assert.Equal(typeof(long), properties[0].ClrType);
            Assert.Equal(typeof(DateTime), properties[1].ClrType);
            Assert.Equal(typeof(string), properties[3].ClrType);
        }
    }
}