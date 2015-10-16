using System.Reflection;
using FluentModelBuilder;
using Microsoft.Data.Entity;

namespace ModelBuilderSample
{
    public class ProjectDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.;Initial Catalog=eftest;Integrated Security=True;");

            // options.UseModel(new FluentModelBuilder(opts => {}).Build());

            // options.BuildModel() // FluentModelBuilder
            //    .Entities()       // FluentEntitiesBuilder
            //    .
            //    .Overrides(x => x.Discover())
            //    .Assemblies()
            //options.BuildModel()
            //    .AddAssemblyContaining<ProjectDbContext>()
            //    .DiscoverEntitiesFromSharedAssemblies(x => x.WithBaseType<Entity>())
            //    ;


            //options.BuildModel()
            //.Entities.Discover(x => {
            //        x.FromSharedAssemblies();
            //    })
            //    .Add<MyEntity>()
            //.Assemblies()
            //    .AddAssembly(typeof(ProjectDbContext).GetTypeInfo().Assembly)
            //    .AddAssemblyContaining<ProjectDbContext>()
            //    .Add(assembly =>
            //    {
            //        assembly.Single(typeof (ProjectDbContext).GetTypeInfo().Assembly);
            //        assembly.Containing<ProjectDbContext>();
            //    });

            //options.BuildModel(opts =>
            //{
            //    // EntityConvention
            //    opts.Entities(x =>
            //    {
            //        // SubConvention: 
            //        x.Discover(discover =>
            //        {
            //            discover.FromAssemblyConvention(opts);
            //            discover.FromAssemblyContaining<Startup>();
            //            discover.WithBaseType<Entity>();
            //        });
            //        x.Add<Entity>();
            //        x.Add<Entity>(e => e.Ignore(c => c.Id));
            //    });

            //    opts.AddEntity<Entity>();
            //    opts.AddEntity<Entity>(x => x.Ignore(c => c.Id));

            //    opts.DiscoverEntities(discover => { });
            //    opts.DiscoverEntitiesFromAssemblyConvention(discover => { });

            //    opts.Overrides(x =>
            //    {
            //        x.Discover(discover =>
            //        {
            //            discover.FromAssemblyConvention(opts);
            //            EntityTypeOverrideDiscoveryConventionOptionsExtensions.FromAssemblyContaining<Startup>(discover);
            //        });
            //    });

            //    opts.DiscoverOverrides(discover => { });
            //    opts.DiscoverOverridesFromAssemblyConvention(discover => { });

            //    opts.Assemblies(x =>
            //    {
            //        x.AddAssemblyContaining<Entity>();
            //    });

            //    opts.AddAssemblyContaining<Entity>();

            //    opts.UseSqlServer();
            //});
        }
    }

    public class MyEntityOne
    {
        
    }
}