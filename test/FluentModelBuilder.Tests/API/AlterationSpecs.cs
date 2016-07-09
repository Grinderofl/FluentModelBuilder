using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Tests.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentAssertions;

namespace FluentModelBuilder.Tests.API
{
    public class AlterationSpecs
    {
        // ReSharper disable once ClassNeverInstantiated.Local
        private class MyEntity
        {
            public int Id { get; set; }
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class AlterationThatAddsSingleEntity : IAutoModelBuilderAlteration
        {
            public void Alter(AutoModelBuilder builder)
            {
                builder.Override<MyEntity>();
            }
        }

        public class WhenAddingSingleAlteration : DbContextTestBase
        {
            [Fact]
            public void adds_alteration()
            {
                var builder = From.Alteration<AlterationThatAddsSingleEntity>();
                var model = CreateModel(builder);
                
                model.GetEntityTypes().First().ClrType.ShouldBeEquivalentTo(typeof(MyEntity));
            }
        }

        public class WhenAddingAlterationThatAddsAlteration : DbContextTestBase
        {
            private class AlterationThatAddsAlteration : IAutoModelBuilderAlteration
            {
                public void Alter(AutoModelBuilder builder)
                {
                    builder.AddAlteration<AlterationThatAddsSingleEntity>();
                }
            }


            [Fact]
            public void does_not_add_alteration()
            {
                var builder = From.Alteration<AlterationThatAddsAlteration>();
                var model = CreateModel(builder);

                model.GetEntityTypes().Count().ShouldBeEquivalentTo(0);
            }
        }
    }
}
