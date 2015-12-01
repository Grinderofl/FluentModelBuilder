using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using Microsoft.Data.Entity;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class OverridingSingleEntityOnModel : TestBase
    {
        public OverridingSingleEntityOnModel(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel().Overrides(x => x.Add(new MyOverride())).WithInMemoryDatabase();
        }

        [Fact]
        public void AddsSingleEntity()
        {
            Assert.Equal(1, Model.GetEntityTypes().OrderBy(x => x.Name).Count());
            Assert.Equal(typeof(SingleEntity), Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(0).ClrType);
        }

        [Fact]
        public void AddsCorrectNumberOfProperties()
        {
            Assert.Equal(2, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(0).GetProperties().OrderBy(x => x.Name).Count());
        }

        [Fact]
        public void AddsProperties()
        {
            var properties = Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(0).GetProperties().OrderBy(x => x.Name).ToArray();
            Assert.Equal("Id", properties[1].Name);
            Assert.Equal("DateProperty", properties[0].Name);
        }
    }
}