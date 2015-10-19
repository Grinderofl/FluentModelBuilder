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
    public class DiscoveringFromSharedAssembliesAndSpecifyingMultipleBaseTypes : TestBase
    {
        public DiscoveringFromSharedAssembliesAndSpecifyingMultipleBaseTypes(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(
                    e =>
                        e.Discover(
                            from =>
                                from.BaseType<EntityBase>()
                                    .BaseType<EntityBaseWannabe>().FromSharedAssemblies()))
                .Overrides(x => x.Discover(from => from.FromSharedAssemblies()))
                .Assemblies(x => x.AddAssemblyContaining<EntityBase>().AddAssemblyContaining<EntityBaseWannabe>())
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsCorrectNumberOfEntities()
        {
            Assert.Equal(5, Model.EntityTypes.Count);
        }

        [Theory]
        [InlineData(typeof(EntityOne), 0)]
        [InlineData(typeof(EntityTwo), 1)]
        [InlineData(typeof(EntityOneWannabe), 2)]
        [InlineData(typeof(EntityTwoWannabe), 3)]
        [InlineData(typeof(SingleEntity), 4)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            Assert.Equal(expected, Model.EntityTypes[index].ClrType);
        }

        [Theory]
        [InlineData(typeof(int), "Id", 0, 0)]
        [InlineData(typeof(string), "NotIgnored", 0, 1)]
        [InlineData(typeof(int), "Id", 1, 0)]
        [InlineData(typeof(int), "Id", 2, 0)]
        [InlineData(typeof(string), "LookAtMe", 2, 1)]
        [InlineData(typeof(int), "Id", 3, 0)]
        [InlineData(typeof(string), "LookAtMeToo", 3, 1)]
        [InlineData(typeof(int), "Id", 4, 0)]
        [InlineData(typeof(DateTime), "DateProperty", 4, 1)]
        public void MapsProperties(Type type, string name, int entityIndex, int propertyIndex)
        {
            Assert.Equal(type, Model.EntityTypes[entityIndex].GetProperties().ElementAt(propertyIndex).ClrType);
            Assert.Equal(name, Model.EntityTypes[entityIndex].GetProperties().ElementAt(propertyIndex).Name);
        }
    }
}