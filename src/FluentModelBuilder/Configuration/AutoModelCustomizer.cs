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

    public class CustomizeParams
    {
        public CustomizeParams(DbContext context, ModelBuilder modelBuilder, BuilderScope scope)
        {
            DbContext = context;
            ModelBuilder = modelBuilder;
            Scope = scope;
        }
        public DbContext DbContext { get; set; }
        public ModelBuilder ModelBuilder { get; set; }
        public BuilderScope Scope { get; set; }
    }
}