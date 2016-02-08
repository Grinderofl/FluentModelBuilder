using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ModelBuilderSample
{
    public class ProjectDbContext : DbContext
    {
        //public ProjectDbContext(IServiceProvider serviceProvider) : base(serviceProvider)
        //{
            
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    //options.ConfigureModel()
        //    //    .Entities(
        //    //        entities =>
        //    //            entities.Discover(from =>
        //    //            from.AssemblyContaining<ProjectDbContext>().BaseType<Entity>())).WithSqlServerDatabase("Data Source=.;Initial Catalog=eftest;Integrated Security=SSPI;");


        //}
        public ProjectDbContext()
        {
            
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    var types = typeof (ProjectDbContext).GetTypeInfo()
        //        .Assembly.GetExportedTypes()
        //        .Where(x => x.Namespace.EndsWith(".Entities"));
        //    foreach (var type in types)
        //        modelBuilder.Entity(type);
        //    base.OnModelCreating(modelBuilder);
        //}
    }

    public class MyEntityOne
    {
        
    }
}