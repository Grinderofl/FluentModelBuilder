using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConventionModelBuilder.Conventions;
using ConventionModelBuilder.Conventions.Options.Extensions;
using ConventionModelBuilder.Conventions.Overrides;
using ConventionModelBuilder.Extensions;
using ConventionModelBuilder.Options.Extensions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Framework.DependencyInjection;

namespace ModelBuilderSample
{
    public class Program
    {
        public void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddEntityFramework().AddDbContext<DbContext>(options =>
            {
                options.BuildModelUsingConventions(opts =>
                {
                    opts.AddEntities(x => x
                        .WithBaseType<Entity>()
                        .FromAssemblyContaining<Program>()
                        );
                    
                    opts.AddOverrides(x => x.FromAssemblyContaining<Program>());
                });
                options.UseSqlServer("Server=.;Initial Catalog=eftest;Integrated Security=True;");
            }).AddSqlServer();

            var provider = services.BuildServiceProvider();
            var context = provider.GetService<DbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Set<TestEntity>().Add(new TestEntity() {Name = "Hi"});
            context.SaveChanges();
            Console.Read();
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
