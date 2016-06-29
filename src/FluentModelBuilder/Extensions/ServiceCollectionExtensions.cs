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
            services.Replace(ServiceDescriptor.Singleton<IModelCustomizer, AutoModelCustomizer>());
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
        ///     Fluently configures Entity Framework for application
        /// </summary>
        /// <param name="builder">Entity Framework Services Builder</param>
        /// <param name="configurationAction">Configuration action to perform</param>
        /// <returns></returns>
        public static EntityFrameworkServicesBuilder Configure(this EntityFrameworkServicesBuilder builder,
            Action<FluentModelBuilderConfiguration> configurationAction)
        {
            var services = builder.GetInfrastructure();
            services.ConfigureEntityFramework(configurationAction);
            return builder;
        }

        /// <summary>
        ///     Fluently configures AutoModelBuilder for Entity Framework for application
        /// </summary>
        /// <param name="optionsBuilder">DbContestOptionsBuilder</param>
        /// <param name="action">AutoModelBuilder</param>
        /// <returns>DbContextOptionsBuilder</returns>
        public static DbContextOptionsBuilder Configure(this DbContextOptionsBuilder optionsBuilder,
            Action<FluentModelBuilderConfiguration> action)
        {
            ((IDbContextOptionsBuilderInfrastructure) optionsBuilder).AddOrUpdateExtension(
                new FluentModelBuilderOptionsExtension());
            var builder = new FluentModelBuilderOptionsBuilder(optionsBuilder);
            builder.Configuration(action);
            return optionsBuilder;
        }

        
        public static IServiceCollection AddAndConfigureDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> action)
        {
            return services.AddAndConfigureDbContext<DbContext>(action, null);
        }

        public static IServiceCollection AddAndConfigureDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> action, AutoModelBuilder builder)
        {
            return services.AddAndConfigureDbContext<DbContext>(action, builder);
        }

        public static IServiceCollection AddAndConfigureDbContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> action, AutoModelBuilder builder) where TContext : DbContext
        {
            services.AddEntityFramework();
            services.AddDbContext<TContext>(((provider, optionsBuilder) =>
            {
                optionsBuilder.UseInternalServiceProvider(provider);
                action(optionsBuilder);
            }));
            if(builder != null)
                services.ConfigureEntityFramework(builder);
            return services;
        }
    }
}