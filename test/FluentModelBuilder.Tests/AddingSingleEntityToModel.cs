//using FluentModelBuilder.InMemory;
using FluentModelBuilder;
using FluentModelBuilder.InMemory;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingSingleEntityToModel : TestBase
    {
        public AddingSingleEntityToModel(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            //((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(new MyExtension());
            //options.UseInMemoryDatabase();
            //options.UseFluentBuilder().UseModelSource(new InMemoryModelSourceBuilder()); //.AddEntity<SingleEntity>(); //.UsingInMemory();
        }

        [Fact]
        public void AddsSingleEntity()
        {
            Assert.Equal(1, Model.EntityTypes.Count);
        }

        [Fact]
        public void AddsProperties()
        {
            Assert.Equal("Id", Model.EntityTypes[0].Name);
            Assert.Equal("StringProperty", Model.EntityTypes[1].Name);
            Assert.Equal("DateProperty", Model.EntityTypes[1].Name);
        }
    }

}
