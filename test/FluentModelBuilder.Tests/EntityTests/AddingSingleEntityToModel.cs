using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity.Metadata;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingSingleEntityToModel : ClassFixture<AddingSingleEntityToModel.Fixture>
    {
        public class Fixture : ModelFixtureBase
        {
            protected override void ConfigureOptions(FluentModelBuilderOptions options)
            {
                options.AddEntity<SingleEntity>();
            }
        }

        public class SingleEntity
        {
            public int Id { get; set; }
            public string Property { get; set; }
            public DateTime Created { get; set; }
        }

        [Fact]
        public void ContainsCorrectEntity()
        {
            Assert.Equal(1, Model.EntityTypes.Count);
            Assert.Equal(typeof (SingleEntity), Model.EntityTypes[0].ClrType);
        }

        [Fact]
        public void ContainsCorrectProperties()
        {
            var properties = Model.EntityTypes[0].GetProperties().OrderBy(x => x.Name).ToArray();

            Assert.Equal("Created", properties[0].Name);
            Assert.Equal(typeof(DateTime), properties[0].ClrType);

            Assert.Equal("Id", properties[1].Name);
            Assert.Equal(typeof(int), properties[1].ClrType);

            Assert.Equal("Property", properties[2].Name);
            Assert.Equal(typeof(string), properties[2].ClrType);
        }

        public AddingSingleEntityToModel(Fixture fixture) : base(fixture)
        {
        }
    }
}
