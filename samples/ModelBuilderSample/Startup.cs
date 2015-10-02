using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConventionModelBuilder.Conventions;
using ConventionModelBuilder.Conventions.Overrides;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace ModelBuilderSample
{
    public class Startup
    {
        public void Main(string[] args)
        {
            var services = new ServiceCollection();
            Configure(services);
            var provider = services.BuildServiceProvider();
            
            var context = provider.GetService<ProjectDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Set<TestEntity>().Add(new TestEntity() {Name = "Hi"});
            context.SaveChanges();
            Console.Read();
        }

        public void Configure(IServiceCollection services)
        {
            services.AddEntityFramework().AddDbContext<ProjectDbContext>().AddSqlServer();
        }
    }

    public class MyConvention : IEntityTypeOverride<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> mapping)
        {
            mapping.Key(x => x.Id);
        }
    }

    public abstract class Entity
    {
        public int Id { get; set; }
    }

    public class TestEntity : Entity
    {
        public string Name { get; set; }
    }

    public class MyEntity
    {
        public int Id { get; set; }
    }
}
