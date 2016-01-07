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
ConfigureModel().Entities(entities => 
{
    entities.Add<SingleEntity>();
    entities.Add(typeof(SingleEntity));
});
```
### Adding and configuring single entities
```c#
ConfigureModel().Entities(entities => 
{
    entities.Add<SingleEntity>(entity => entity.Ignore(p => p.PropertyIWantToIgnore()));
});
```
*Note: entity can be configured multiple times using `Add<>`*
### Discoverying entities by scanning assemblies
FluentModelBuilder offers an assembly scanning contributor, accessed via the `Discover()` extension method.

### Find types that aren't abstract and have the specified base type from two different assemblies
```c#
ConfigureModel().Entities(entities => 
{
    entities.Discover(entity => 
      entity
        .BaseType<BaseEntity>()
        .AssemblyContaining<AnEntity>()
        .AddAssembly(typeof(SecondEntity).GetTypeInfo().Assembly)
    );
});
```



### TODO
