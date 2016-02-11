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
            _configuration = configuration;
        }

        public override void Customize(ModelBuilder modelBuilder, DbContext dbContext)
        {
            var preModelCreatingParams = new CustomizeParams(dbContext, modelBuilder, BuilderScope.PreModelCreating);
            _configuration.Apply(preModelCreatingParams);
            base.Customize(modelBuilder, dbContext);
            var postModelCreatingParams = new CustomizeParams(dbContext, modelBuilder, BuilderScope.PostModelCreating);
            _configuration.Apply(postModelCreatingParams);
        }
    }
}