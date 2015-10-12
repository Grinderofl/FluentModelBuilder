using System.Collections.Generic;
using FluentModelBuilder.Options;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder.v2
{
    public class EntitiesBuilder : ICoreBuilder
    {
        public EntitiesBuilder(FluentModelBuilder builder)
        {
            Builder = builder;
        }

        public FluentModelBuilder Builder { get; }
        public IList<IDescriptor> Sources { get; } = new List<IDescriptor>();
        public void ApplyServices(IServiceCollection services)
        {
            foreach(var source in Sources)
                source.ApplyServices(services);
        }
    }

    public interface ICoreBuilder
    {
        FluentModelBuilder Builder { get; }
    }

    public static class EntitiesBuilderExtensions
    {
        public static EntitiesBuilder Add<T>(this EntitiesBuilder builder)
        {
            builder.Sources.Add(new SingleEntityDescriptor(typeof(T)));
            return builder;
        }
    }

    /// <summary>
    /// Builds model using conventions
    /// </summary>
    public class FluentModelBuilder
    {
        public FluentModelBuilderOptions Options { get; } = new FluentModelBuilderOptions();

        public EntitiesBuilder Entities { get; }

        public void ApplyServices(IServiceCollection services)
        {
            Entities.ApplyServices(services);
            //foreach (var coreDescriptor in CoreDescriptors)
            //{
            //    coreDescriptor.ApplyServices(services);
            //}

            services.AddSingleton<IModelBuilderApplier, ModelBuilderApplier>();
            Options.ModelSourceApplier.Apply(services);
            //services.Replace(ServiceDescriptor.Singleton<IModelSource, FluentModelSource>());
        }

        public FluentModelBuilder()
        {
            Entities = new EntitiesBuilder(this);
        }
    }
}
