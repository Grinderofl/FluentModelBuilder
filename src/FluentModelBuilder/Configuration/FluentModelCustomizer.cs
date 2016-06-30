using System;
using FluentModelBuilder.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FluentModelBuilder.Configuration
{
    public class FluentModelCustomizer : ModelCustomizer
    {
        private readonly FluentModelBuilderConfiguration _configuration;

        public FluentModelCustomizer(FluentModelBuilderConfiguration configuration)
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

        private void ApplyConfiguration(BuilderContext @params)
        {
            _configuration.Apply(@params);
        }

        private static BuilderContext BuildPostModelCreatingParameters(ModelBuilder modelBuilder, DbContext dbContext)
            => new BuilderContext(dbContext, modelBuilder, BuilderScope.PostModelCreating);

        private static BuilderContext BuildPreModelCreatingParameters(ModelBuilder modelBuilder, DbContext dbContext)
            => new BuilderContext(dbContext, modelBuilder, BuilderScope.PreModelCreating);
    }
}