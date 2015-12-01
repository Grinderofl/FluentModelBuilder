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
            Assert.Equal(6, Model.GetEntityTypes().OrderBy(x => x.Name).Count());
        }

        [Theory]
        [InlineData(typeof(EntityOne), 4)]
        [InlineData(typeof(EntityTwo), 5)]
        [InlineData(typeof(EntityBaseWannabe), 0)]
        [InlineData(typeof(EntityOneWannabe), 1)]
        [InlineData(typeof(EntityTwoWannabe), 2)]
        [InlineData(typeof(SingleEntity), 3)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            var entities = Model.GetEntityTypes().OrderBy(x => x.Name).ToList();
            Assert.Equal(expected, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(typeof(int), "Id", 4, 0)]
        [InlineData(typeof(string), "NotIgnored", 4, 1)]

        [InlineData(typeof(int), "Id", 5, 0)]

        [InlineData(typeof(int), "Id", 0, 0)]

        [InlineData(typeof(int), "Id", 1, 0)]
        [InlineData(typeof(string), "LookAtMe", 1, 1)]

        [InlineData(typeof(int), "Id", 2, 0)]
        [InlineData(typeof(string), "LookAtMeToo", 2, 1)]

        [InlineData(typeof(int), "Id", 3, 1)]
        [InlineData(typeof(DateTime), "DateProperty", 3, 0)]
        public void MapsProperties(Type type, string name, int entityIndex, int propertyIndex)
        {
            Assert.Equal(type, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(entityIndex).GetProperties().OrderBy(x => x.Name).ElementAt(propertyIndex).ClrType);
            Assert.Equal(name, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(entityIndex).GetProperties().OrderBy(x => x.Name).ElementAt(propertyIndex).Name);
        }
    }
}