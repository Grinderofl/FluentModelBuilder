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
    public class AddingAndConfiguringSingleEntityToModel : TestBase
    {
        public AddingAndConfiguringSingleEntityToModel(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(e => e.Add<SingleEntity>(c => c.Property<long>("CustomProperty")))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsSingleEntity()
        {
            Assert.Equal(1, Model.GetEntityTypes().Count());
            Assert.Equal(typeof(SingleEntity), Model.GetEntityTypes().ElementAt(0).ClrType);
        }

        [Fact]
        public void MapsProperties()
        {
            var properties = Model.GetEntityTypes().ElementAt(0).GetProperties().ToArray();
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal("CustomProperty", properties[1].Name);
            Assert.Equal("DateProperty", properties[2].Name);
            Assert.Equal("StringProperty", properties[3].Name);

            Assert.Equal(typeof(int), properties[0].ClrType);
            Assert.Equal(typeof(long), properties[1].ClrType);
            Assert.Equal(typeof(DateTime), properties[2].ClrType);
            Assert.Equal(typeof(string), properties[3].ClrType);
        }
    }
}