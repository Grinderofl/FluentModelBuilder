using FluentModelBuilder.Conventions.Core.Options.Extensions;
using FluentModelBuilder.Conventions.EntityConvention.Options.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options.Extensions;
using FluentModelBuilder.SqlServer.Extensions;
using Microsoft.Data.Entity;

namespace ModelBuilderSample
{
    public class ProjectDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.;Initial Catalog=eftest;Integrated Security=True;");

            // options.UseModel(new FluentModelBuilder(opts => {}).Build());
            options.BuildModel(opts =>
            {
                // EntityConvention
                opts.Entities(x =>
                {
                    // SubConvention: 
                    x.Discover(discover =>
                    {
                        discover.FromAssemblyConvention(opts);
                        discover.FromAssemblyContaining<Startup>();
                        discover.WithBaseType<Entity>();
                    });
                    x.Add<Entity>();
                    x.Add<Entity>(e => e.Ignore(c => c.Id));
                });

                opts.AddEntity<Entity>();
                opts.AddEntity<Entity>(x => x.Ignore(c => c.Id));

                opts.DiscoverEntities(discover => { });
                opts.DiscoverEntitiesFromCommonAssemblies(discover => { });
                
                opts.Overrides(x =>
                {
                    x.Discover(discover =>
                    {
                        discover.FromAssemblyConvention(opts);
                        discover.FromAssemblyContaining<Startup>();
                    });
                });

                opts.DiscoverOverrides(discover => { });
                opts.DiscoverOverridesFromCommonAssemblies(discover => { });

                opts.Assemblies(x =>
                {
                    x.AddAssemblyContaining<Entity>();
                });

                opts.AddAssemblyContaining<Entity>();

                opts.UseSqlServer();
            });
        }
    }

    public class MyEntityOne
    {
        
    }
}