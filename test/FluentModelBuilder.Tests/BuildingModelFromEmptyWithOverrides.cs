using System;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelFromEmptyWithOverrides : TestBase<EmptyFixture, DbContext>
    {
        public BuildingModelFromEmptyWithOverrides(EmptyFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(0, typeof(SingleEntity))]
        [InlineData(1, typeof(EntityOne))]
        public void MapsEntity(int index, Type expectedType)
        {
            Assert.Equal(expectedType, EntityTypes.ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(0, 0, "DateProperty")]
        [InlineData(0, 1, "Id")]
        [InlineData(0, 2, "StringProperty")]
        
        [InlineData(1, 0, "Id")]
        [InlineData(1, 1, "IgnoredInOverride")]
        [InlineData(1, 2, "NotIgnored")]
        public void MapsEntityProperty(int elementIndex, int propertyIndex, string name)
        {
            var properties = GetProperties(elementIndex);
            Assert.Equal(name, properties.ElementAt(propertyIndex).Name);
        }

    }

    public class EmptyFixture : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(From.Empty(new TestConfiguration()).Override<EntityOne>().Override<SingleEntity>());
        }
    }
}