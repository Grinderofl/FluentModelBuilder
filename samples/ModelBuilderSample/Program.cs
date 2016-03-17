using System;
using FluentModelBuilder;
using FluentModelBuilder.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Tests;
using Microsoft.EntityFrameworkCore;

namespace ModelBuilderSample
{
    public class Program
    {
        public void Main(string[] args)
        {
            //var prov =
            //    new ServiceCollection().AddScoped<TestClass>().AddSingleton(new TestDependency() {Name = "Hello"}).BuildServiceProvider();

            //var testClass = prov.GetService<TestClass>();

            var fixture = new MultipleAssemblyFixtureWithOverrides();
            var test = new BuildingModelFromMultipleAssembliesWithOverrides(fixture);
            test.MapsEntityProperty(0, 0, "Id");
            //test.MapsEntityProperty("AccessFailedCount", 2, 0);

            IServiceProvider provider = BuildServiceProvider();
            try
            {
                //var testClass = provider.GetService<TestClass>();
                var context = provider.GetService<DbContext>();
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
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<DbContext>(
                    (p, x) => x.UseInMemoryDatabase().UseInternalServiceProvider(p));
            services.AddScoped<TestClass>();
            //services
            //    .AddEntityFrameworkSqlServer()
            //    .AddDbContext<ProjectDbContext>(
            //        (p, c) => c.UseSqlServer("Data Source=.;Initial Catalog=eftest;Integrated Security=SSPI;").UseInternalServiceProvider(p));
            //services.ConfigureEntityFramework(
            //    mappings =>
            //        mappings.Add(
            //            From.AssemblyOf<Program>(new ProgramConfiguration()).UseOverridesFromAssemblyOf<Program>()));
        }
    }

    public class TestClass
    {
        public TestDependency Dependency { get; set; }


        protected TestClass() : this(new TestDependency())
        {
            
        }

        public TestClass(TestDependency dependency)
        {
            Dependency = dependency;
        }
    }

    public class TestDependency
    {
        public string Name { get; set; }
    }
}
