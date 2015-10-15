using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder
{
    public static class FluentModelBuilderEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddFluentBuilderContributor(this EntityFrameworkServicesBuilder builder)
        {
            var service = builder.GetService();
            service.TryAddScoped<IFluentBuilderContributor, FluentBuilderContributor>();
            return builder;
        }


        public static EntityFrameworkServicesBuilder AddModelSourceProvider<TProvider>(
            this EntityFrameworkServicesBuilder builder) where TProvider : IModelSourceProvider, new()
        {
            var service = builder.AddFluentBuilderContributor();
            var provider = new TProvider();
            provider.ApplyServices(service);
            return service;
        }
    }
}