using System;
using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Sqlite.Extensions;
using FluentModelBuilder.Tests.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingAndConfiguringSingleEntityToTwoProvidersAndDbContextsModelsSqliteAndInMemory : IClassFixture<Fixture>
    {
        protected IModel DbContextModel;
        protected IModel SecondContextModel;

        public AddingAndConfiguringSingleEntityToTwoProvidersAndDbContextsModelsSqliteAndInMemory(Fixture fixture)
        {
            ConfigureServices(fixture.Services);
            var provider = fixture.Services.BuildServiceProvider();
            DbContextModel = provider.GetService<DbContext>().Model;
            SecondContextModel = provider.GetService<SecondContext>().Model;
        }

        public class SecondContext : DbContext
        {
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddDbContext<DbContext>(ConfigureOptions)
                .AddDbContext<SecondContext>(ConfigureOptions2)
                .AddSqliteFluentModelBuilder()
                .AddInMemoryFluentModelBuilder();
        }

        protected void ConfigureOptions2(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(e => e.Add<OtherSingleEntity>(c => c.Property<long>("CustomOtherProperty")))
                .WithSqliteDatabase("Data Source=:memory:;Version=3;New=True;");
        }

        protected void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(e => e.Add<SingleEntity>(c => c.Property<long>("CustomProperty")))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsSingleEntityToFirstContext()
        {
            Assert.Equal(1, DbContextModel.GetEntityTypes().Count());
            Assert.Equal(typeof(SingleEntity), DbContextModel.GetEntityTypes().ElementAt(0).ClrType);
        }

        [Fact]
        public void AddsSingleEntityToSecondContext()
        {
            Assert.Equal(1, SecondContextModel.GetEntityTypes().Count());
            Assert.Equal(typeof(OtherSingleEntity), SecondContextModel.GetEntityTypes().ElementAt(0).ClrType);
        }

        [Fact]
        public void MapsPropertiesToFirstContext()
        {
            var properties = DbContextModel.GetEntityTypes().ElementAt(0).GetProperties().ToArray();
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal("CustomProperty", properties[1].Name);
            Assert.Equal("DateProperty", properties[2].Name);
            Assert.Equal("StringProperty", properties[3].Name);

            Assert.Equal(typeof(int), properties[0].ClrType);
            Assert.Equal(typeof(long), properties[1].ClrType);
            Assert.Equal(typeof(DateTime), properties[2].ClrType);
            Assert.Equal(typeof(string), properties[3].ClrType);
        }

        [Fact]
        public void MapsPropertiesToSecondContext()
        {
            var properties = SecondContextModel.GetEntityTypes().ElementAt(0).GetProperties().ToArray();
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal("CustomOtherProperty", properties[1].Name);
            Assert.Equal("OtherStringProperty", properties[2].Name);

            Assert.Equal(typeof(int), properties[0].ClrType);
            Assert.Equal(typeof(long), properties[1].ClrType);
            Assert.Equal(typeof(string), properties[2].ClrType);
        }
    }
}