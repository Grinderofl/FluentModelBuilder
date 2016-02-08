using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class SingleAssemblyFixture : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(From.AssemblyOf<EntityBase>(new TestConfiguration()));
        }
    }
    
    public class BuildingModelFromSingleAssembly : TestBase<SingleAssemblyFixture, DbContext>
    {
        public BuildingModelFromSingleAssembly(SingleAssemblyFixture fixture) : base(fixture)
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
}
