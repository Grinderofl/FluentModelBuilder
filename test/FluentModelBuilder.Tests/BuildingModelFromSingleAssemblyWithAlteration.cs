using System;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelFromSingleAssemblyWithAlteration : TestBase<SingleAssemblyFixtureWithAlteration, DbContext>
    {
        public BuildingModelFromSingleAssemblyWithAlteration(SingleAssemblyFixtureWithAlteration fixture) : base(fixture)
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
        [InlineData(0, 1, "IgnoredInOverride")]
        [InlineData(0, 2, "NotIgnored")]

        [InlineData(1, 0, "Id")]
        public void MapsEntityProperty(int elementIndex, int propertyIndex, string name)
        {
            var properties = GetProperties(elementIndex);
            Assert.Equal(name, properties.ElementAt(propertyIndex).Name);
        }
    }

    public class TestAlteration : IAutoModelBuilderAlteration
    {
        public void Alter(AutoModelBuilder builder)
        {
            builder.UseOverridesFromAssemblyOf<EntityOneWannabe>();
        }
    }

    public class SingleAssemblyFixtureWithAlteration : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(
                From.AssemblyOf<EntityBase>(new TestConfiguration()).Alterations(a => a.Add<TestAlteration>()));
        }
    }
}