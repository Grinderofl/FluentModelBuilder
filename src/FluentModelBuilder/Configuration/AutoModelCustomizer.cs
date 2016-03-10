using System;
using FluentModelBuilder.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FluentModelBuilder.Configuration
{
    public class AutoModelCustomizer : ModelCustomizer
    {
        private readonly FluentModelBuilderConfiguration _configuration;

        public AutoModelCustomizer(FluentModelBuilderConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;
        }

        public override void Customize(ModelBuilder modelBuilder, DbContext dbContext)
        {
            var preModelCreatingParams = BuildPreModelCreatingParameters(modelBuilder, dbContext);
            var postModelCreatingParams = BuildPostModelCreatingParameters(modelBuilder, dbContext);

            ApplyConfiguration(preModelCreatingParams);
            base.Customize(modelBuilder, dbContext);
            ApplyConfiguration(postModelCreatingParams);
        }

        private void ApplyConfiguration(CustomizeParams @params)
        {
            _configuration.Apply(@params);
        }

        private static CustomizeParams BuildPostModelCreatingParameters(ModelBuilder modelBuilder, DbContext dbContext)
            => new CustomizeParams(dbContext, modelBuilder, BuilderScope.PostModelCreating);

        private static CustomizeParams BuildPreModelCreatingParameters(ModelBuilder modelBuilder, DbContext dbContext)
            => new CustomizeParams(dbContext, modelBuilder, BuilderScope.PreModelCreating);
    }
}