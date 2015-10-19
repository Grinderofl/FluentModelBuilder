using System;
using System.Reflection;
using FluentModelBuilder;
using FluentModelBuilder.Core.Contributors.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.SqlServer.Extensions;
using Microsoft.Data.Entity;

namespace ModelBuilderSample
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(
                    entities =>
                        entities.Discover(from =>
                        from.AssemblyContaining<ProjectDbContext>().BaseType<Entity>())).WithSqlServerDatabase("Data Source=.;Initial Catalog=eftest;Integrated Security=SSPI;");


        }
    }

    public class MyEntityOne
    {
        
    }
}