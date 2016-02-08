using System;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.TestTarget;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Tests
{

    public class BuildingModelFromSingleAssemblyWithOverrides : TestBase<SingleAssemblyFixtureWithOverrides, DbContext>
    {
        public BuildingModelFromSingleAssemblyWithOverrides(SingleAssemblyFixtureWithOverrides fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(0, typeof(EntityOne))]
        [InlineData(1, typeof(EntityTwo))]
        public void MapsEntity(int index, Type expectedType)
        {
            Assert.Equal(expectedType, EntityTypes.ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(0, 0, "Id")]
        [InlineData(0, 1, "NotIgnored")]

        [InlineData(1, 0, "Id")]
        public void MapsEntityProperty(int elementIndex, int propertyIndex, string name)
        {
            var properties = GetProperties(elementIndex);
            Assert.Equal(name, properties.ElementAt(propertyIndex).Name);
        }
    }

    public class SingleAssemblyFixtureWithOverrides : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(
                From.AssemblyOf<EntityBase>(new TestConfiguration()).UseOverridesFromAssemblyOf<EntityBase>());
        }
    }
}