using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConventionModelBuilder.Extensions;
using ConventionModelBuilder.Options;
using ConventionModelBuilder.TestTarget;
using Microsoft.Data.Entity.Metadata;
using Xunit;

namespace ConventionModelBuilder.Tests
{

    public class AddingEntityToModel : IClassFixture<AddingEntityToModel.Fixture>
    {
        public class Fixture 
        {
            public Fixture()
            {
                var options = new ConventionModelBuilderOptions();
                options.AddEntity<EntityOne>();
                Model = new ConventionModelBuilder(options).Build();
            }

            public IModel Model { get; set; }
        }

        private readonly Fixture _fixture;

        public AddingEntityToModel(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ContainsSingleEntity()
        {
            Assert.True(_fixture.Model.EntityTypes.Count == 1);
        }

        [Fact]
        public void ContainsCorrectEntity()
        {
            Assert.True(_fixture.Model.EntityTypes[0].ClrType == typeof(EntityOne));
        }

        [Fact]
        public void ContainsCorrectProperties()
        {
            var properties = _fixture.Model.EntityTypes[0].GetProperties().ToList();
            Assert.True(properties.Any(x => x.Name == "Id"));
            Assert.True(properties.Any(x => x.Name == "IgnoredInOverride"));
            Assert.True(properties.Any(x => x.Name == "NotIgnored"));
        }
    }
}
