using FluentModelBuilder.Conventions.Options.Extensions;
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
            options.BuildModelUsingConventions(opts =>
            {
                opts.AddEntities(x => x.WithBaseType<Entity>()
                    .FromAssemblyContaining<Startup>()
                    );
                opts.UseSqlServer();
                opts.AddOverrides(x => x.FromAssemblyContaining<Startup>());
            });
        }
    }
}