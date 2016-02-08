using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelFromSingleAssemblyWithOverride : TestBase<SingleAssemblyFixtureWithOverride, DbContext>
    {
        public BuildingModelFromSingleAssemblyWithOverride(SingleAssemblyFixtureWithOverride fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(0, typeof(SingleEntity))]
        [InlineData(1, typeof(EntityOne))]
        [InlineData(2, typeof(EntityTwo))]
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

        [InlineData(1, 0, "Id")]
        public void MapsEntityProperty(int elementIndex, int propertyIndex, string name)
        {
            var properties = GetProperties(elementIndex);
            Assert.Equal(name, properties.ElementAt(propertyIndex).Name);
        }
    }

    public class SingleAssemblyFixtureWithOverride : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(
                From.AssemblyOf<EntityBase>(new TestConfiguration()).Override<SingleEntity>());
        }
    }
}
