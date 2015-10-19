using System;
using System.Linq;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.SqlServer.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingAndConfiguringSingleEntityToModelFromDbContext : IClassFixture<DbContextFixture<AddingAndConfiguringSingleEntityToModelFromDbContext.TestContext>>
    {
        protected IModel Model;

        public AddingAndConfiguringSingleEntityToModelFromDbContext(DbContextFixture<TestContext> fixture)
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
                options.UseInMemoryDatabase();
                //options.ConfigureModel()
                //.Entities(e => e.Add<SingleEntity>(c => c.Property<long>("CustomProperty")))
                //.WithInMemoryDatabase();
            }
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework().AddDbContext<TestContext>().AddInMemoryFluentProvider();
        }

        [Fact]
        public void AddsSingleEntity()
        {
            Assert.Equal(1, Model.EntityTypes.Count);
            Assert.Equal(typeof(SingleEntity), Model.EntityTypes[0].ClrType);
        }

        [Fact]
        public void MapsProperties()
        {
            var properties = Model.EntityTypes[0].GetProperties().ToArray();
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal("CustomProperty", properties[1].Name);
            Assert.Equal("DateProperty", properties[2].Name);
            Assert.Equal("StringProperty", properties[3].Name);

            Assert.Equal(typeof(int), properties[0].ClrType);
            Assert.Equal(typeof(long), properties[1].ClrType);
            Assert.Equal(typeof(DateTime), properties[2].ClrType);
            Assert.Equal(typeof(string), properties[3].ClrType);
        }
    }

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