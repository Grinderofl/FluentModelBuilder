using System;
using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class DiscoveringOverridesFromSingleAssembly : TestBase
    {
        public DiscoveringOverridesFromSingleAssembly(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Overrides(e => e.Discover(from => from.AssemblyContaining<EntityOne>()))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsCorrectNumberOfEntities()
        {
            Assert.Equal(1, Model.EntityTypes.Count);
        }

        [Theory]
        [InlineData(typeof(EntityOne), 0)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            Assert.Equal(expected, Model.EntityTypes[index].ClrType);
        }

        [Fact]
        public void MapsProperties()
        {
            Assert.Equal(2, Model.EntityTypes.SelectMany(x => x.GetProperties()).Count());
        }
    }
}