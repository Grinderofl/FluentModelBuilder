using System;
using System.Reflection;
using FluentModelBuilder;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;


namespace ModelBuilderSample
{
    public class Program
    {
        public void Main(string[] args)
        {
            var services = new ServiceCollection();
            Configure(services);
            var provider = services.BuildServiceProvider();
            try
            {
                var context = provider.GetService<ProjectDbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Set<TestEntity>().Add(new TestEntity() {Name = "Hi"});
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
            Console.Read();
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
                        From.AssemblyOf<TestEntity>(new ProgramConfiguration()).UseOverridesFromAssemblyOf<TestEntity>()));
            //services.ConfigureContext(x => x.AddAlteration(new EntityTypeAlterationRegistryItem(typeof(TestEntity), typeof(TestEntityOverride))));
        }
    }

    public class ProgramConfiguration : DefaultEntityAutoConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return base.ShouldMap(type) && type.GetTypeInfo().IsSubclassOf(typeof(Entity));
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

    public class TestEntityOverride : IEntityTypeOverride<TestEntity>
    {
        public void Override(EntityTypeBuilder<TestEntity> mapping)
        {
            mapping.Property<string>("MyProperty");
        }
    }

    public class MyEntity
    {
        public int Id { get; set; }
    }
}
