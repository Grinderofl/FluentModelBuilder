## Introduction

**FluentModelBuilder** provides an entry point for Entity Framework 7 Model customization programmatically, instead of using `DbSet` or `OnModelCreating` override. This gives the DbContext customization a more modular and automatic approach.

### Goals

[Entity Framework 7](https://github.com/aspnet/EntityFramework) is a ground-up rework of Microsoft's ORM platform. Unfortunately, it also means all of the features previous EF's had will need to be re-implemented. That's a lot of work, understandably so, but sadly a whole lot of features are still missing. One of the biggest offenders is the ability to customize your backing model from anywhere else than your subclassed DbContext, where in EF6 and earlier, there were several means of doing this, currently there is only one officially supported way - building your model yourself, but it has the pitfall of not being considerate with the DbContext's `DbSet` properties.

Cue **FluentModelBuilder**: It adds a model convention which updates the `InternalModelBuilder` used by `ModelBuilder` in `OnModelCreating` method. FluentModelBuilder runs prior to all other configuration, so anything that is in `OnModelCreating` of a DbContext overrides convention-based configuration.

# Examples

Here are a couple of examples to show you it works:

## Example 1 - DbContext with specific entities
### Subclassing DbContext

```c#
// ApplicationContext.cs
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

// Startup.cs
public class Startup
{
	public void Configure(IServiceCollection services)
    {
    	services.AddEntityFramework()
        	.AddDbContext<ApplicationContext>(x => x.UseInMemoryDatabase())
            .AddInMemory();
    }
}
```

### FluentModelBuilder equivalent
```c#
// Startup.cs
public class Startup
{
	public void Configure(IServiceCollection services)
    {
   		services.AddEntityFramework()
        	.AddDbContext<DbCotnext>(x => x.UseInMemoryDatabase())
            .AddInMemory();

        services.ConfigureEntityFramework(mappings =>
        	mappings.Add(From.Empty()
            	.Override<EntityOne>()
                .Override<EntityTwo>()
                .Override<EntityThree>()));
    }
}
```

## Example 2 - DbContext with discovered entities
### Subclassing DbContext
```c#
// ApplicationContext.cs
public class ApplicationContext : DbContext
{
  	protected override void OnModelCreating(ModelBuilder builder)
  	{
    	var types = typeof (ApplicationContext).GetTypeInfo()
                .Assembly.GetExportedTypes()
                .Where(x => x.GetTypeInfo().Namespace.EndsWith(".Entities"));
            foreach (var type in types)
                modelBuilder.Entity(type);
  	}
}

// Startup.cs
public class Startup
{
	public void Configure(IServiceCollection services)
    {
    	services.AddEntityFramework()
        	.AddDbContext<ApplicationContext>(x => x.UseInMemoryDatabase())
            .AddInMemory();
    }
}
```

### FluentModelBuilder equivalent
```c#
// Startup.cs
public class Startup
{
	public void Configure(IServiceCollection services)
	{
   	services.AddEntityFramework()
			.AddDbContext<DbCotnext>(x => x.UseInMemoryDatabase())
			.AddInMemory();
        services.ConfigureEntityFramework(mappings =>
        	mappings.Add(From.AssemblyOf<Startup>(new EntityConfiguration()))
    }
}

// EntityConfiguration.cs
public class EntityConfiguration : DefaultEntityAutoConfiguration
{
	public override bool ShouldMap(Type type)
    {
    	return type.GetTypeInfo().Namespace.EndsWith(".Entities");
    }
}
```