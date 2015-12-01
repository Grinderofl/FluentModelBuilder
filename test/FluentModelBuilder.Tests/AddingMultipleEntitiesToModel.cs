using System;
using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using Microsoft.Data.Entity;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingMultipleEntitiesToModel : TestBase
    {
        public AddingMultipleEntitiesToModel(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(e => e.Add<SingleEntity>().Add<OtherSingleEntity>())
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsBothEntities()
        {
            Assert.Equal(2, Model.GetEntityTypes().Count());
            Assert.Equal(typeof(OtherSingleEntity), Model.GetEntityTypes().ElementAt(0).ClrType);
            Assert.Equal(typeof(SingleEntity), Model.GetEntityTypes().ElementAt(1).ClrType);
        }

        [Theory]
        [InlineData(0, 0, typeof(int), "Id")]
        [InlineData(0, 1, typeof(string), "OtherStringProperty")]

        [InlineData(1, 0, typeof(int), "Id")]
        [InlineData(1, 1, typeof(DateTime), "DateProperty")]
        [InlineData(1, 2, typeof(string), "StringProperty")]
        public void MapsProperties(int entityIndex, int propertyIndex, Type propertyType, string propertyName)
        {
            Assert.Equal(propertyType, Model.GetEntityTypes().ElementAt(entityIndex).GetProperties().ElementAt(propertyIndex).ClrType);
            Assert.Equal(propertyName, Model.GetEntityTypes().ElementAt(entityIndex).GetProperties().ElementAt(propertyIndex).Name);
        }
    }
}