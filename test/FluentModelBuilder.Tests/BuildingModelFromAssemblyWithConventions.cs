using System;
using System.Linq;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentModelBuilder.TestTarget;
using FluentModelBuilder.Configuration;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelFromAssemblyWithConventions : TestBase<AssemblyWithConventionsFixture, DbContext>
    {
        public BuildingModelFromAssemblyWithConventions(AssemblyWithConventionsFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(1, typeof(EntityOne))]
        [InlineData(0, typeof(SingleEntity))]
        public void MapsEntity(int index, Type expectedType)
        {
            Assert.Equal(expectedType, EntityTypes.ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(0, 0, "DateProperty")]
        [InlineData(0, 1, "Id")]

        [InlineData(1, 0, "Id")]
        [InlineData(1, 1, "IgnoredInOverride")]
        [InlineData(1, 2, "NotIgnored")]
        public void MapsEntityProperty(int elementIndex, int propertyIndex, string name)
        {
            var properties = GetProperties(elementIndex);
            Assert.Equal(name, properties.ElementAt(propertyIndex).Name);
        }

    }

    public class AssemblyWithConventionsFixture : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(From.Empty(new TestConfiguration()).UseConventionsFromAssemblyOf<SingleEntity>());
        }
    }

    public class Convention : Conventions.IModelBuilderConvention
    {
        public void Override(ModelBuilder builder)
        {
            builder.Entity<EntityOne>();
            builder.Entity<SingleEntity>();
        }
    }
}