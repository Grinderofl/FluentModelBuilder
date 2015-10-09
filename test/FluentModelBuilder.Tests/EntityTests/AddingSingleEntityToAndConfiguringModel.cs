using System;
using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity.Metadata;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingSingleEntityToAndConfiguringModel : ClassFixture<AddingSingleEntityToAndConfiguringModel.Fixture>
    {
        
        [Fact]
        public void ContainsSingleEntity()
        {
            Assert.True(Model.EntityTypes.Count == 1);
        }

        [Fact]
        public void ContainsCorrectEntity()
        {
            Assert.Equal(1, Model.EntityTypes.Count);
            Assert.Equal(typeof(AddingSingleEntityToModel.SingleEntity), Model.EntityTypes[0].ClrType);
        }

        [Fact]
        public void ContainsCorrectProperties()
        {
            var properties = Model.EntityTypes[0].GetProperties().OrderBy(x => x.Name).ToArray();

            Assert.Equal("Created", properties[0].Name);
            Assert.Equal(typeof(DateTime), properties[0].ClrType);

            Assert.Equal("Id", properties[1].Name);
            Assert.Equal(typeof(int), properties[1].ClrType);
        }

        [Fact]
        public void DoesNotContainIgnoredProperty()
        {
            Assert.False(Model.EntityTypes.Any(c => c.Name == "Property"));
        }

        public class Fixture : ModelFixtureBase
        {
            protected override void ConfigureOptions(FluentModelBuilderOptions options)
            {
                options.AddEntity<AddingSingleEntityToModel.SingleEntity>(x => x.Ignore(p => p.Property));
            }
        }

        public AddingSingleEntityToAndConfiguringModel(Fixture fixture) : base(fixture)
        {
        }
    }
}