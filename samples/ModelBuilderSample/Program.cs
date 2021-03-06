﻿using System;
using FluentModelBuilder;
using FluentModelBuilder.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentModelBuilder.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ModelBuilderSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceProvider provider = BuildServiceProvider();
            try
            {
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

        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            Configure(services);
            return services.BuildServiceProvider();
        }

        public static void Configure(IServiceCollection services)
        {
            services.AddDbContext<DbContext>(
                x => x.UseInMemoryDatabase().Configure(f => f.UsingAssemblyOf<TestClass>(new ProgramConfiguration())));
            //services.AddEntityFrameworkInMemoryDatabase();
            //services.AddAndConfigureDbContext(x => x.UseInMemoryDatabase(),
            //    From.AssemblyOf<TestClass>(new ProgramConfiguration()));
            //services.AddDbContext<DbContext>(
            //    (p, x) => x.UseInMemoryDatabase().UseInternalServiceProvider(p));
            //services.AddAndConfigureDbContext<DbContext>(x => x.UseInMemoryDatabase(), From.AssemblyOf<TestClass>(new ProgramConfiguration()));
            //services.ConfigureEntityFramework(From.AssemblyOf<TestClass>(new ProgramConfiguration()));
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
