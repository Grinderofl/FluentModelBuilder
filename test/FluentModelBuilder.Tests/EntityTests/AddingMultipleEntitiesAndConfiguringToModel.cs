using System;
using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using Xunit;

namespace FluentModelBuilder.Tests.EntityTests
{
    public class AddingMultipleEntitiesAndConfiguringToModel : ClassFixture<AddingMultipleEntitiesAndConfiguringToModel.Fixture>
    {

        [Fact]
        public void ContainsCorrectEntities()
        {
            Assert.Equal(2, Model.EntityTypes.Count);
            Assert.Equal(typeof(FirstEntity), Model.EntityTypes[0].ClrType);
            Assert.Equal(typeof(SecondEntity), Model.EntityTypes[1].ClrType);
        }

        [Fact]
        public void ContainsPropertiesForFirstEntity()
        {
            var properties = Model.EntityTypes[0].GetProperties().OrderBy(x => x.Name).ToArray();
            Assert.Equal(2, properties.Length);
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal(typeof(int), properties[0].ClrType);
            Assert.Equal("Property", properties[1].Name);
            Assert.Equal(typeof(string), properties[1].ClrType);
        }

        [Fact]
        public void DoesNotContainIgnoredPropertiesForFirstEntity()
        {
            Assert.False(Model.EntityTypes.Any(x => x.Name == "Created"));
        }

        [Fact]
        public void ContainsPropertiesForSecondEntity()
        {
            var properties = Model.EntityTypes[1].GetProperties().OrderBy(x => x.Name).ToArray();
            Assert.Equal(2, properties.Length);
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal(typeof(int), properties[0].ClrType);
            Assert.Equal("Property", properties[1].Name);
            Assert.Equal(typeof(long), properties[1].ClrType);
        }

        [Fact]
        public void DoesNotContainIgnoredPropertiesForSecondEntity()
        {
            Assert.False(Model.EntityTypes.Any(x => x.Name == "Modified"));
        }

        public class Fixture : ModelFixtureBase
        {
            protected override void ConfigureOptions(FluentModelBuilderOptions options)
            {
                options.AddEntity<FirstEntity>(x => x.Ignore(p => p.Created))
                    .AddEntity<SecondEntity>(x => x.Ignore(p => p.Modified));
            }
        }

        public class FirstEntity
        {
            public int Id { get; set; }
            public string Property { get; set; }
            public DateTime Created { get; set; }
        }

        public class SecondEntity
        {
            public int Id { get; set; }
            public long Property { get; set; }
            public DateTime Modified { get; set; }
        }

        public AddingMultipleEntitiesAndConfiguringToModel(Fixture fixture) : base(fixture)
        {
        }
    }
}