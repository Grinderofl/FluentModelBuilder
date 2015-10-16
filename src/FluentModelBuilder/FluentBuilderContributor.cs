using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder
{
    public class FluentBuilderContributor : IFluentBuilderContributor, IAccessor<IServiceProvider>
    {
        private DbContextOptions _options;
        private IServiceProvider _serviceProvider;

        protected FluentBuilderContributor()
        {
            Initialize(DbContextActivator.ServiceProvider);
        }

        public FluentBuilderContributor(IServiceProvider serviceProvider)
        {
            Initialize(serviceProvider);
        }

        private void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _options = GetService<DbContextOptions>();
        }

        public virtual void Contribute(ModelBuilder modelBuilder)
        {
            var extension = _options.FindExtension<FluentModelBuilderExtension>();
            ApplyEntities(extension.Entities, modelBuilder);
        }

        protected virtual void ApplyEntities(EntitiesBuilder builder, ModelBuilder modelBuilder)
        {
            builder.Apply(modelBuilder);
        }

        protected T GetService<T>() => _serviceProvider.GetService<T>();
        IServiceProvider IAccessor<IServiceProvider>.Service => _serviceProvider;
    }
}