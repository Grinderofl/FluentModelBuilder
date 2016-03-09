using System;
using FluentModelBuilder;
using FluentModelBuilder.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentModelBuilder.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ModelBuilderSample
{
    public class Program
    {
        public void Main(string[] args)
        {
            IServiceProvider provider = BuildServiceProvider();
            try
            {
                var context = provider.GetService<ProjectDbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Set<TestEntity>().Add(new TestEntity() { Name = "Hi" });
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Read();
        }

        private IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            Configure(services);
            return services.BuildServiceProvider();
        }

        public void Configure(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddDbContext<ProjectDbContext>(
                    c => c.UseSqlServer("Data Source=.;Initial Catalog=eftest;Integrated Security=SSPI;"))
                .AddSqlServer();
            services.ConfigureEntityFramework(
                mappings =>
                    mappings.Add(
                        From.AssemblyOf<Program>().UseOverridesFromAssemblyOf<Program>()));
        }
    }
}
