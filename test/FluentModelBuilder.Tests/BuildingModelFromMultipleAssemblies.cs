using System;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.TestTarget;
using Xunit;
using System.Linq;
using FluentModelBuilder.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelFromMultipleAssemblies : TestBase<MultiAssemblyFixture, DbContext>
    {
        public BuildingModelFromMultipleAssemblies(MultiAssemblyFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(0, typeof(EntityOneWannabe))]
        [InlineData(1, typeof(EntityOne))]
        [InlineData(2, typeof(EntityTwo))]
        public void MapsEntity(int index, Type expectedType)
        {
            Assert.Equal(expectedType, EntityTypes.ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(0, 0, "Id")]
        [InlineData(0, 1, "LookAtMe")]

        [InlineData(1, 0, "Id")]
        [InlineData(1, 1, "IgnoredInOverride")]
        [InlineData(1, 2, "NotIgnored")]

        [InlineData(2, 0, "Id")]
        public void MapsEntityProperty(int elementIndex, int propertyIndex, string name)
        {
            var properties = GetProperties(elementIndex);
            Assert.Equal(name, properties.ElementAt(propertyIndex).Name);
        }
    }

    public class MultiAssemblyFixture : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(
                From.AssemblyOf<EntityBase>(new TestConfiguration()).AddEntityAssemblyOf<EntityOneWannabe>());
        }
    }
}