using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder
{
    public static class FluentBuilderEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddFluentBuilder(this EntityFrameworkServicesBuilder builder)
        {
            var service = builder.GetService();
            service.TryAddScoped<IFluentBuilderApplier, FluentBuilderApplier>();
            return builder;
        }

        public static EntityFrameworkServicesBuilder AddFluentBuilderProvider<TProvider>(
            this EntityFrameworkServicesBuilder builder) where TProvider : IProvider, new()
        {
            var service = builder.AddFluentBuilder();
            var applier = new TProvider();
            applier.ApplyServices(service);
            return service;
        }
    }
}