using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingSingleEntityToModelInNewCustomDbContext : IClassFixture<AddingSingleEntityToModelInNewCustomDbContext.TestFixture>
    {
        public class TestFixture
        {
            public TestFixture()
            {
                var context = new TestContext();
                Model = context.Model;
            }

            public IModel Model { get; set; }
        }

        public class TestContext : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.ConfigureModel().Entities(x => x.Add<SingleEntity>()).WithInMemoryDatabase();
            }
        }

        public AddingSingleEntityToModelInNewCustomDbContext(TestFixture fixture)
        {
            Model = fixture.Model;
        }

        protected IModel Model;

        [Fact]
        public void AddsSingleEntity()
        {
            Assert.Equal(1, Model.EntityTypes.Count);
            Assert.Equal(typeof(SingleEntity), Model.EntityTypes[0].ClrType);
        }

        [Fact]
        public void AddsProperties()
        {
            var properties = Model.EntityTypes[0].GetProperties().ToArray();
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal("DateProperty", properties[1].Name);
            Assert.Equal("StringProperty", properties[2].Name);
        }
    }
}