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
            builder.Configure(configurationAction);
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
            builder.Configure(builders);
            return builder;
        }
    }
}