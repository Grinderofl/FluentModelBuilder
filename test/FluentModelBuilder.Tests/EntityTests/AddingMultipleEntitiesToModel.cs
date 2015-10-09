using System;
using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using Xunit;

namespace FluentModelBuilder.Tests.EntityTests
{
    public class AddingMultipleEntitiesToModel : ClassFixture<AddingMultipleEntitiesToModel.Fixture>
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
            Assert.Equal(3, properties.Length);
            Assert.Equal("Created", properties[0].Name);
            Assert.Equal(typeof(DateTime), properties[0].ClrType);
            Assert.Equal("Id", properties[1].Name);
            Assert.Equal(typeof(int), properties[1].ClrType);
            Assert.Equal("Property", properties[2].Name);
            Assert.Equal(typeof(string), properties[2].ClrType);
        }

        [Fact]
        public void ContainsPropertiesForSecondEntity()
        {
            var properties = Model.EntityTypes[1].GetProperties().OrderBy(x => x.Name).ToArray();
            Assert.Equal(3, properties.Length);
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal(typeof(int), properties[0].ClrType);
            Assert.Equal("Modified", properties[1].Name);
            Assert.Equal(typeof(DateTime), properties[1].ClrType);
            Assert.Equal("Property", properties[2].Name);
            Assert.Equal(typeof(long), properties[2].ClrType);
        }

        public class Fixture : ModelFixtureBase
        {
            protected override void ConfigureOptions(FluentModelBuilderOptions options)
            {
                options.AddEntity<FirstEntity>().AddEntity<SecondEntity>();
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

        public AddingMultipleEntitiesToModel(Fixture fixture) : base(fixture)
        {
        }
    }
}