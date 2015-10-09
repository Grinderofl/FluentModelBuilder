# FluentModelBuilder

Provides a fluent interface for creating `IModel` for EntityFramework 7 DbContext

## Goals

The default way of creating the backing model to use with EntityFramework 7 DbContext __requires__ subclassing:

```c#
public class ApplicationContext : DbContext
{
  public DbSet<EntityOne> EntityOnes { get; set; } // Available as both context.EntityOnes and context.Set<EntityOne>();
  public DbSet<EntityTwo> EntityTwos { get; set; } // Available as both context.EntityTwos and context.Set<EntityTwo>();
  
  protected override void OnModelCreating(ModelBuilder builder)
  {
    // Alternatively:
    builder.Entity<EntityThree>(); // Available as context.Set<EntityThree>();
  }
}
```

FluentModelBuilder aims to provide an alternative way - via fluent syntax, plus some built-in assembly discovery conventions.

## Quick start

Easiest way to add an impromptu DbContext is while setting up the container, like in `Configure(IServiceCollection)` method in an ASP.NET 5 Web Application:

```c#
public void Configure(IServiceCollection services)
{
  services.AddEntityFramework()
    .AddSqlServer()
    .AddDbContext<DbContext>(dbContextOptions => {
    
      dbContextOptions.UseSqlServer("..."); // Connection String
      
      dbContextOptions.BuildModel(model => 
        model
          .AddAssemblyContaining<EntityOne>() // Scans this assembly
          .DiscoverEntitiesFromAssemblyConvention(discover => discover.WithBaseType<EntityBase>()) // adds entities that subclass from EntityBase from provided assembly
          .DiscoverOverridesFromAssemblyConvention() // discovers IEntityTypeOverride<>s from provided assembly
          .UseSqlServer()); 
    });
}
```

Alternatively, you can use the `OnConfiguring(DbContextOptions)` override in your DbContext:

```c#
public class MyContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder builder)
  {
    builder.BuildModelForSqlServer("ConnectionString", model =>
      model.(...); // No need to .UseSqlServer();
    );
  }
}
```

