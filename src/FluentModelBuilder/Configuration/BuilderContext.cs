using System;
using FluentModelBuilder.Builder;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Configuration
{
    public class BuilderContext
    {
        public BuilderContext(DbContext context, ModelBuilder modelBuilder, BuilderScope scope)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            DbContext = context;
            ModelBuilder = modelBuilder;
            Scope = scope;
        }

        public DbContext DbContext { get; set; }
        public ModelBuilder ModelBuilder { get; set; }
        public BuilderScope Scope { get; set; }
    }
}