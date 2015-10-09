using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Conventions.Assemblies.Options.Extensions;
using FluentModelBuilder.Conventions.Entities.Options.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using FluentModelBuilder.TestTarget;
using Xunit;

namespace FluentModelBuilder.Tests.AssemblyTests
{
    public class AddingEntitiesFromConventions : ClassFixture<AddingEntitiesFromConventions.Fixture>
    {
        [Fact]
        public void ContainsCorrectEntities()
        {
            Assert.Equal(2, Model.EntityTypes.Count);
            Assert.Equal(typeof(EntityOne), Model.EntityTypes[0].ClrType);
            Assert.Equal(typeof(EntityTwo), Model.EntityTypes[1].ClrType);
        }

        [Fact]
        public void EntityOneContainsCorrectProperties()
        {
            var properties = Model.EntityTypes[0].GetProperties().OrderBy(x => x.Name).ToArray();
            Assert.Equal(3, properties.Length);
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal(typeof(int), properties[0].ClrType);

            Assert.Equal("IgnoredInOverride", properties[1].Name);
            Assert.Equal(typeof(string), properties[1].ClrType);

            Assert.Equal("NotIgnored", properties[2].Name);
            Assert.Equal(typeof(string), properties[2].ClrType);
        }

        [Fact]
        public void EntityTwoContainsCorrectProperties()
        {
            var properties = Model.EntityTypes[1].GetProperties().OrderBy(x => x.Name).ToArray();
            Assert.Equal(1, properties.Length);
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal(typeof(int), properties[0].ClrType);
        }

        [Fact]
        public void DoesNotAddBaseType()
        {
            Assert.False(Model.EntityTypes.Any(x => x.ClrType == typeof(EntityBase)));
        }

        public class Fixture : ModelFixtureBase
        {
            protected override void ConfigureOptions(FluentModelBuilderOptions options)
            {
                options.Assemblies(d => d.AddAssemblyContaining<EntityOne>());
                options.DiscoverEntitiesFromAssemblyConvention(x => x.WithBaseType<EntityBase>());
            }
        }

        public AddingEntitiesFromConventions(Fixture fixture) : base(fixture)
        {
        }
    }
}
