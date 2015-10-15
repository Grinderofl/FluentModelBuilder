using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;

namespace FluentModelBuilder
{
    public class FluentBuilderApplier : IFluentBuilderApplier, IAccessor<IServiceProvider>
    {
        private LazyRef<DbContextOptions> _options;
        private IServiceProvider _serviceProvider;

        protected FluentBuilderApplier()
        {
            Initialize(DbContextActivator.ServiceProvider);
        }

        public FluentBuilderApplier(IServiceProvider serviceProvider)
        {
            Initialize(serviceProvider);
        }

        private void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _options = new LazyRef<DbContextOptions>(this.GetService<DbContextOptions>);
        }

        public virtual void Apply(ModelBuilder modelBuilder, DbContext context)
        {
            var extension = _options.Value.FindExtension<FluentModelBuilderExtension>();
            ApplyEntities(extension.Entities, modelBuilder);
        }

        protected virtual void ApplyEntities(EntitiesBuilder builder, ModelBuilder modelBuilder)
        {
            builder.Apply(modelBuilder);
        }

        IServiceProvider IAccessor<IServiceProvider>.Service => _serviceProvider;
    }
}