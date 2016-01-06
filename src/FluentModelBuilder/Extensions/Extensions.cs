using System;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentModelBuilder
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

        public static void ConfigureEntityFramework(this IServiceCollection services, Action<FluentModelBuilderConfiguration> configurationAction)
        {
            var configuration = new FluentModelBuilderConfiguration();
            configurationAction(configuration);
            configuration.Alterations.Add(new FluentModelBuilderConventionSetAlteration(configuration));
            services.AddInstance(configuration);
            services.Replace(ServiceDescriptor.Singleton<ICoreConventionSetBuilder, AutoCoreConventionSetBuilder>());
        }
    }
}