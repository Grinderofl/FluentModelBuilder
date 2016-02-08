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
            _configuration.Apply(modelBuilder, BuilderScope.PreModelCreating);
            base.Customize(modelBuilder, dbContext);
            _configuration.Apply(modelBuilder, BuilderScope.PostModelCreating);
        }
    }
}