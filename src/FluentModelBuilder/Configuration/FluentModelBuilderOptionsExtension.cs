using System;
using System.Collections.Generic;
using System.Linq;
using FluentModelBuilder.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Configuration
{
    public class FluentModelBuilderOptionsExtension : IDbContextOptionsExtension
    {
        internal IList<Action<FluentModelBuilderConfiguration>> ConfigurationActions =
            new List<Action<FluentModelBuilderConfiguration>>();

        public FluentModelBuilderOptionsExtension()
        {
        }

        public FluentModelBuilderOptionsExtension(FluentModelBuilderOptionsExtension copyFrom)
        {
            ConfigurationActions = copyFrom.ConfigurationActions;
        }

        public void ApplyServices(IServiceCollection services)
        {
            if (ConfigurationActions.Any())
                services.ConfigureEntityFramework(conf =>
                {
                    foreach (var action in ConfigurationActions)
                        action(conf);
                });
        }

        public void Configure(Action<FluentModelBuilderConfiguration> action)
        {
            ConfigurationActions.Add(action);
        }
    }
}