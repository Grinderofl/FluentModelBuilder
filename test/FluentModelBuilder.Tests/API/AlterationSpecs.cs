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
using FluentModelBuilder.Builder.Sources;
using FluentModelBuilder.Conventions;

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
        private class AlterationThatOverridesSingleEntity : IAutoModelBuilderAlteration
        {
            public void Alter(AutoModelBuilder builder)
            {
                builder.Override<MyEntity>();
            }
        }

        public class WhenAddingSingleAlterationThatOverridesSingleEntity : DbContextTestBase
        {
            [Fact]
            public void applies_override()
            {
                var builder = From.Alteration<AlterationThatOverridesSingleEntity>();
                var model = CreateModel(builder);
                
                model.GetEntityTypes().First().ClrType.ShouldBeEquivalentTo(typeof(MyEntity));
            }
        }

        public class WhenAddingAlterationThatAddsAlteration : DbContextTestBase
        {
            // ReSharper disable once ClassNeverInstantiated.Local
            private class AlterationThatAddsAlteration : IAutoModelBuilderAlteration
            {
                public void Alter(AutoModelBuilder builder)
                {
                    builder.AddAlteration<AlterationThatOverridesSingleEntity>();
                }
            }


            [Fact]
            public void does_not_execute_alteration()
            {
                var builder = From.Alteration<AlterationThatAddsAlteration>();
                var model = CreateModel(builder);

                model.GetEntityTypes().Count().ShouldBeEquivalentTo(0);
            }
        }

        public class WhenAddingAlterationThatAddsConvention : DbContextTestBase
        {
            // ReSharper disable once ClassNeverInstantiated.Local
            private class AlterationThatAddsConvention : IAutoModelBuilderAlteration
            {
                public void Alter(AutoModelBuilder builder)
                {
                    builder.UseConvention<ConventionThatAddsSingleEntity>();
                }
            }

            // ReSharper disable once ClassNeverInstantiated.Local
            private class ConventionThatAddsSingleEntity : IModelBuilderConvention
            {
                public void Apply(ModelBuilder modelBuilder)
                {
                    modelBuilder.Entity<MyEntity>();
                }
            }

            [Fact]
            public void applies_convention()
            {
                var builder = From.Alteration<AlterationThatAddsConvention>();
                var model = CreateModel(builder);

                model.GetEntityTypes().First().ClrType.ShouldBeEquivalentTo(typeof(MyEntity));
            }
        }

        //public class WhenAddingAlterationThatAddsTypeSource : DbContextTestBase
        //{
        //    // ReSharper disable once ClassNeverInstantiated.Local
        //    private class AlterationThatAddsTypeSource : IAutoModelBuilderAlteration
        //    {
        //        public void Alter(AutoModelBuilder builder)
        //        {
        //            builder.AddTypeSource<MyEntityTypeSource>();
        //        }
        //    }

        //    // ReSharper disable once ClassNeverInstantiated.Local
        //    private class MyEntityTypeSource : ITypeSource
        //    {
        //        public IEnumerable<Type> GetTypes()
        //        {
        //            yield return typeof(MyEntity);
        //        }
        //    }

        //    [Fact]
        //    public void adds_types()
        //    {
        //        var builder = From.Alteration<AlterationThatAddsTypeSource>(new DefaultEntityAutoConfiguration());
        //        var model = CreateModel(builder);

        //        model.GetEntityTypes().First().ClrType.ShouldBeEquivalentTo(typeof(MyEntity));
        //    }
        //}
    }
}
