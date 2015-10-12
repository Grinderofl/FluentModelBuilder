using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    public class DiscoveryDescriptor : IDescriptor
    {
        public DiscoveryOptions Options { get; } = new DiscoveryOptions();

        public void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton(typeof (ITypeSource), service =>
            {
                var assemblies = Options.Assemblies;
                if (Options.UseSharedAssemblies)
                {
                    var sharedSource = service.GetRequiredService<ISharedAssemblySource>();
                    foreach (var assembly in sharedSource.GetAssemblies())
                        assemblies.Add(assembly);
                }
                var typeSource = new DiscoveryTypeSource(assemblies, Options.Criterias);
                return typeSource;
            });
        }
    }
}