using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Relational.Conventions;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace FluentModelBuilder.Tests.Conventions
{
    public class PluralizingTableNameConventionTests
    {
        [Fact]
        public void PluralizingTableNameGeneratingConvention_Pluralizes()
        {
            var convention = new PluralizingTableNameGeneratingConvention();
            var builder =
                new ModelBuilder(
                    new CoreConventionSetBuilder(
                        new CoreConventionSetBuilderDependencies(
                            new CoreTypeMapper(new CoreTypeMapperDependencies()))).CreateConventionSet());
            builder.Entity<SingleEntity>();

            convention.Apply(builder);

            Assert.Equal("SingleEntities", builder.Entity<SingleEntity>().Metadata.Relational().TableName);
        }
    }
}
