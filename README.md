# FluentModelBuilder

Provides a provider for Entity Framework 7 that helps configure `ModelBuilder` from outside `DbContext`, with a fluent interface, convention-based assembly scanning, and useful set of extension methods to suit different convention situations of model alterations.

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

FluentModelBuilder aims to provide an alternative way to add and configure those entities on your data model as well as add old-style class-based `EntityTypeConfiguration<>`-esque mapping overrides for each entity. It also enables including a well-defined non-abstract DbContext in the service provider, with a fully configured non-prebuilt `IModel`, which is not possible with standard Entity Framework 7.

# Quick start

Easiest way to get started is to set up an impromptu DbContext while configuring services, like in `Configure(IServiceCollection)` method in an ASP.NET 5 Web Application:

```c#
public void Configure(IServiceCollection services)
{
  services.AddEntityFramework()
    .AddSqlServerFluentModelBuilder()
    .AddDbContext<DbContext>(dbContextOptions => {
      dbContextOptions.ConfigureModel() 
        .DiscoverEntitiesFromSharedAssemblies(entity => // Scans assemblies for entities
          entity
            .Namespace(ns => ns.EndsWith("Domain")) // Namespace criterion
            .BaseType<EntityBase>()) // Only if base type matches
        .DiscoverOverridesFromSharedAssemblies() // Scans assemblies for overrides
        .AddAssemblyContaining<YourEntity>() // Specifies which assembly to scan
        .WithSqlServerDatabase("... connectionstring ..."); // Specifies to use SQL Server as well as connection string
}
```

Alternatively, you can use the `OnConfiguring(DbContextOptions)` override in your DbContext:

```c#
public class MyContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder builder)
  {
    builder.ConfigureModel()
      .DiscoverEntitiesFromSharedAssemblies(entity => // Scans assemblies for entities
        entity
          .Namespace(ns => ns.EndsWith("Domain")) // Namespace criterion
          .BaseType<EntityBase>()) // Only if base type matches
      .DiscoverOverridesFromSharedAssemblies() // Scans assemblies for overrides
      .AddAssemblyContaining<YourEntity>() // Specifies which assembly to scan
      .WithSqlServerDatabase("... connectionstring ..."); // Specifies to use SQL Server as well as connection string
  }
}
```

# Usage guide
## Entity configuration
Entities are configured via the `Entities()` method, which exposes `Action<EntitiesBuilder>` as parameter
```c#
ConfigureModel().Entities(x => {});
```

### Adding single entities
```c#
Entities(entities => 
{
    entities.Add<SingleEntity>();
    entities.Add(typeof(SingleEntity));
});
```

### TODO


## NuGet:

* https://www.nuget.org/packages/FluentModelBuilder/
* https://www.nuget.org/packages/FluentModelBuilder.Sqlite/
* https://www.nuget.org/packages/FluentModelBuilder.SqlServer/
* https://www.nuget.org/packages/FluentModelBuilder.InMemory/

## Build status
[![Build status](https://ci.appveyor.com/api/projects/status/yccb8ad2msd26bad/branch/master?svg=true)](https://ci.appveyor.com/project/Grinderofl/fluentmodelbuilder/branch/master)
