using System;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Configuration
{
    public class FluentModelBuilderOptionsExtension : IDbContextOptionsExtension
    {
        public virtual AutoModelBuilder AutoModelBuilder { get; set; }

        public FluentModelBuilderOptionsExtension()
        {
        }

        public FluentModelBuilderOptionsExtension(FluentModelBuilderOptionsExtension copyFrom)
        {
            AutoModelBuilder = copyFrom.AutoModelBuilder;
        }

        public void ApplyServices(IServiceCollection services)
        {
            if(AutoModelBuilder != null)
                services.ConfigureEntityFramework(c => c.Add(AutoModelBuilder));
        }
    }

    public class FluentModelBuilderOptionsBuilder
    {
        private readonly DbContextOptionsBuilder _builder;

        public FluentModelBuilderOptionsBuilder(DbContextOptionsBuilder builder)
        {
            _builder = builder;
        }

        public virtual FluentModelBuilderOptionsBuilder Add(AutoModelBuilder builder)
            => SetOption(x => x.AutoModelBuilder = builder);
        

        protected virtual FluentModelBuilderOptionsBuilder SetOption(
            Action<FluentModelBuilderOptionsExtension> setAction)
        {
            var extension = new FluentModelBuilderOptionsExtension();
            setAction(extension);
            ((IDbContextOptionsBuilderInfrastructure)_builder).AddOrUpdateExtension(extension);
            return this;
        }
    }


}