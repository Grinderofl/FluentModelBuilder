using System;
using System.Linq;
using FluentModelBuilder.Core.Contributors.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class DiscoveringFromSharedAssembliesAndSpecifyingMultipleBaseTypesWithShortSyntax : TestBase
    {
        public DiscoveringFromSharedAssembliesAndSpecifyingMultipleBaseTypesWithShortSyntax(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .AddAssemblyContaining<EntityBase>()
                .AddAssemblyContaining<EntityBaseWannabe>()
                .DiscoverEntitiesFromSharedAssemblies(e => e.BaseType<EntityBase>().BaseType<EntityBaseWannabe>())
                .DiscoverOverridesFromSharedAssemblies()
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsCorrectNumberOfEntities()
        {
            Assert.Equal(6, Model.GetEntityTypes().Count());
        }

        [Theory]
        [InlineData(typeof(EntityOne), 0)]
        [InlineData(typeof(EntityTwo), 1)]
        [InlineData(typeof(EntityBaseWannabe), 2)]
        [InlineData(typeof(EntityOneWannabe), 3)]
        [InlineData(typeof(EntityTwoWannabe), 4)]
        [InlineData(typeof(SingleEntity), 5)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            Assert.Equal(expected, Model.GetEntityTypes().ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(typeof(int), "Id", 0, 0)]
        [InlineData(typeof(string), "NotIgnored", 0, 1)]

        [InlineData(typeof(int), "Id", 1, 0)]

        [InlineData(typeof(int), "Id", 2, 0)]

        [InlineData(typeof(int), "Id", 3, 0)]
        [InlineData(typeof(string), "LookAtMe", 3, 1)]

        [InlineData(typeof(int), "Id", 4, 0)]
        [InlineData(typeof(string), "LookAtMeToo", 4, 1)]

        [InlineData(typeof(int), "Id", 5, 0)]
        [InlineData(typeof(DateTime), "DateProperty", 5, 1)]
        public void MapsProperties(Type type, string name, int entityIndex, int propertyIndex)
        {
            Assert.Equal(type, Model.GetEntityTypes().ElementAt(entityIndex).GetProperties().ElementAt(propertyIndex).ClrType);
            Assert.Equal(name, Model.GetEntityTypes().ElementAt(entityIndex).GetProperties().ElementAt(propertyIndex).Name);
        }
    }
}