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
        private readonly IList<Action<FluentModelBuilderConfiguration>> _configurationActions =
            new List<Action<FluentModelBuilderConfiguration>>();

        public FluentModelBuilderOptionsExtension()
        {
        }

        public FluentModelBuilderOptionsExtension(FluentModelBuilderOptionsExtension copyFrom)
        {
            _configurationActions = copyFrom._configurationActions;
        }
        
        public long GetServiceProviderHashCode()
        {
            return _configurationActions.GetHashCode();
        }

        public void Validate(IDbContextOptions options)
        {
            
        }

        public string LogFragment { get; set; }

        public void Configure(Action<FluentModelBuilderConfiguration> action)
        {
            _configurationActions.Add(action);
        }

        public bool ApplyServices(IServiceCollection services)
        {
            if (_configurationActions.Any())
                services.ConfigureEntityFramework(conf =>
                {
                    foreach (var action in _configurationActions)
                        action(conf);
                });

            return true;
        }
    }
}