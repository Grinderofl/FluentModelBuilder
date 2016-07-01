using System;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FluentModelBuilder.Extensions
{
    public static class EntityFrameworkServicesBuilderExtensions
    {
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
        ///     Fluently configures Entity Framework for application
        /// </summary>
        /// <param name="builder">Entity Framework Services Builder</param>
        /// <param name="builders">AutoModelBuilders to add</param>
        /// <returns></returns>
        public static EntityFrameworkServicesBuilder Configure(this EntityFrameworkServicesBuilder builder,
            params AutoModelBuilder[] builders)
        {
            var services = builder.GetInfrastructure();
            services.ConfigureEntityFramework(builders);
            return builder;
        }
    }
}