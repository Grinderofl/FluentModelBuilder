# FluentModelBuilder

Provides an extension for Entity Framework 7 to configure `ModelBuilder` from outside `DbContext`, with a fluent interface, convention-based assembly scanning and set of useful extension methods to suit different situations of model alterations.

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

FluentModelBuilder aims to provide an alternative way to add and configure those entities on your data model.

## Quick start

Easiest way to add an impromptu DbContext is while setting up the container, like in `Configure(IServiceCollection)` method in an ASP.NET 5 Web Application:

```c#
public void Configure(IServiceCollection services)
{
  services.AddEntityFramework()
    .AddSqlServer()
    .AddDbContext<DbContext>(dbContextOptions => {
      dbContextOptions.ConfigureModel() 
        .DiscoverEntities(entity => // Scans assemblies for entities
          entity
            .Namespace(ns => ns.EndsWith("Domain")) // Namespace criterion
            .BaseType<EntityBase>()) // Only if base type matches
        .DiscoverOverrides() // Scans assemblies for overrides
        .FromAssemblyContaining<YourEntity>() // Specifies which assembly to scan
        .WithSqlServer("... connectionstring ..."); // Specifies to use SQL Server as well as connection string
}
```

Alternatively, you can use the `OnConfiguring(DbContextOptions)` override in your DbContext:

```c#
public class MyContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder builder)
  {
    builder.BuildModel(); // ...
  }
}
```
