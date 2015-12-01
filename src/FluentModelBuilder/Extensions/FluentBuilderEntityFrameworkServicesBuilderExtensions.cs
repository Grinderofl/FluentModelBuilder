using FluentModelBuilder.Core;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentModelBuilder.Extensions
{
    public static class FluentModelBuilderEntityFrameworkServicesBuilderExtensions
    {
        /// <summary>
        /// Adds required services for Fluent ModelBuilder
        /// </summary>
        /// <param name="builder"><see cref="EntityFrameworkServicesBuilder"/></param>
        /// <returns><see cref="EntityFrameworkServicesBuilder"/></returns>
        public static EntityFrameworkServicesBuilder AddFluentModelBuilder(this EntityFrameworkServicesBuilder builder)
        {
            var service = builder.GetInfrastructure();
            service.TryAddScoped<IFluentModelBuilder, Core.FluentModelBuilder>();
            return builder;
        }

        /// <summary>
        /// Adds a Fluent ModelBuilder ModelSource Provider to services
        /// </summary>
        /// <typeparam name="TProvider">Type of <see cref="IModelSourceProvider"/> to add</typeparam>
        /// <param name="builder"><see cref="EntityFrameworkServicesBuilder"/></param>
        /// <returns><see cref="EntityFrameworkServicesBuilder"/></returns>
        public static EntityFrameworkServicesBuilder AddFluentModelBuilder<TProvider>(
            this EntityFrameworkServicesBuilder builder) where TProvider : IModelSourceProvider, new()
        {
            var service = builder.AddFluentModelBuilder();
            var provider = new TProvider();
            provider.ApplyServices(service);
            return service;
        }
    }
}