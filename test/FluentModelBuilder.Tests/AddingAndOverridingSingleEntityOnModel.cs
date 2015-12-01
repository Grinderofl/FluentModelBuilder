using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using Microsoft.Data.Entity;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingAndOverridingSingleEntityOnModel : TestBase
    {
        public AddingAndOverridingSingleEntityOnModel(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(x => x.Add<SingleEntity>(e => e.Property(typeof(string), "CustomProperty")))
                .Overrides(x => x.Add(new MyOverride()))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsSingleEntity()
        {
            Assert.Equal(1, Model.GetEntityTypes().Count());
            Assert.Equal(typeof(SingleEntity), Model.GetEntityTypes().ElementAt(0).ClrType);
        }

        [Fact]
        public void AddsCorrectNumberOfProperties()
        {
            Assert.Equal(3, Model.GetEntityTypes().ElementAt(0).GetProperties().Count());
        }

        [Fact]
        public void AddsProperties()
        {
            var properties = Model.GetEntityTypes().ElementAt(0).GetProperties().ToArray();
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal("CustomProperty", properties[1].Name);
            Assert.Equal("DateProperty", properties[2].Name);
        }
    }
}