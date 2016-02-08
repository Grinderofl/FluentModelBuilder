using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
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
            return type.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
        }

        public static bool IsDbContextType(this Type type)
        {
            return typeof (DbContext).IsAssignableFrom(type);
        }

        /// <summary>
        /// Fluently configures DbContext for application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationAction"></param>
        public static void ConfigureEntityFramework(this IServiceCollection services,
            Action<FluentModelBuilderConfiguration> configurationAction)
        {
            var configuration = new FluentModelBuilderConfiguration();
            configurationAction(configuration);
            //configuration.Alterations.Add(new FluentModelBuilderConventionSetAlteration(configuration));
            services.AddSingleton(configuration);
            services.Replace(ServiceDescriptor.Singleton<IModelCustomizer, AutoModelCustomizer>());
            //services.Replace(ServiceDescriptor.Singleton<ICoreConventionSetBuilder, AutoCoreConventionSetBuilder>());
        }
    }
}