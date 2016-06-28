using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentModelBuilder.Extensions
{
    public static class Extensions
    {
        public static bool IsEntityTypeOverrideType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>) &&
                   type.GetGenericArguments().Length > 0;
        }

        public static bool ClosesInterface(this Type type, Type interfaceType)
        {
            return
                type.GetInterfaces()
                    .Any(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
        }

        public static bool IsDbContextType(this Type type)
        {
            return typeof (DbContext).IsAssignableFrom(type);
        }

        /// <summary>
        ///     Fluently configures Entity Framework for application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationAction"></param>
        public static void ConfigureEntityFramework(this IServiceCollection services,
            Action<FluentModelBuilderConfiguration> configurationAction)
        {
            var configuration = new FluentModelBuilderConfiguration();
            configurationAction(configuration);
            services.AddSingleton(configuration);
            services.Replace(ServiceDescriptor.Singleton<IModelCustomizer, AutoModelCustomizer>());
        }

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
    }
}