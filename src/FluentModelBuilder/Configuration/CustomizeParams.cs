using FluentModelBuilder.Builder;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Configuration
{
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