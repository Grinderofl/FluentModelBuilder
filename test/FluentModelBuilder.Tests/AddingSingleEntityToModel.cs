using FluentModelBuilder.InMemory;
using FluentModelBuilder.v2;
using Microsoft.Data.Entity;
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
            options.BuildModel().UsingInMemory().AddEntity<SingleEntity>();
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