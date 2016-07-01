using System;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentModelBuilder.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Fluently configures Entity Framework for application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationAction"></param>
        public static IServiceCollection ConfigureEntityFramework(this IServiceCollection services,
            Action<FluentModelBuilderConfiguration> configurationAction)
        {
            var configuration = new FluentModelBuilderConfiguration();
            configurationAction(configuration);
            services.AddSingleton(configuration);
            services.Replace(ServiceDescriptor.Singleton<IModelCustomizer, FluentModelCustomizer>());
            return services;
        }

        /// <summary>
        ///     Fluently configures Entity Framework for application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builders"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureEntityFramework(this IServiceCollection services, params AutoModelBuilder[] builders) 
            => services.ConfigureEntityFramework(x =>
            {
                foreach (var builder in builders)
                    x.Add(builder);
            });

        

        /// <summary>
        ///     Adds anonymous DbContext to service collection
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="action">Configuration action</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services,
            Action<DbContextOptionsBuilder> action) => services.AddDbContext<DbContext>(action);

        /// <summary>
        ///     Adds anonymous DbContext to service collection and configures it using fluent API
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <param name="fluentAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddAndConfigureDbContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> action, Action<FluentModelBuilderConfiguration> fluentAction) where TContext : DbContext
        {
            services.AddEntityFramework();
            services.AddDbContext<TContext>(((provider, optionsBuilder) =>
            {
                optionsBuilder.UseInternalServiceProvider(provider);
                action(optionsBuilder);
            }));
            services.ConfigureEntityFramework(fluentAction);
            return services;
        }

        /// <summary>
        ///     Adds anonymous DbContext to service collection and configures it using From API
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <param name="builders"></param>
        /// <returns></returns>
        public static IServiceCollection AddAndConfigureDbContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> action, params AutoModelBuilder[] builders) where TContext : DbContext
        {
            services.AddEntityFramework();
            services.AddDbContext<TContext>(((provider, optionsBuilder) =>
            {
                optionsBuilder.UseInternalServiceProvider(provider);
                action(optionsBuilder);
            }));
            services.ConfigureEntityFramework(builders);
            return services;
        }
    }
}