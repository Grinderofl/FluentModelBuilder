using Microsoft.Data.Entity;

namespace ModelBuilderSample
{
    public class OtherContext : DbContext
    {
        public DbSet<TestEntity> TestEntity { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=eftest2;Integrated Security=True;");
        }
    }
}