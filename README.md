# FluentModelBuilder
Alternative for creating DbContext: using fluent interface and possible conventions


## Basic usage as part of servicecollection

```c#
public void Configure(IServiceCollection services)
{
  services.AddEntityFramework()
    .AddSqlServer()
    .AddDbContext<DbContext>(dbContextOptions => {
    
      dbContextOptions.UseSqlServer(...);
      
      dbContextOptions.BuildModel(fluently =>
      
        fluently.AddEntities(entities => {
          entities.WithBaseType<EntityBase>();
          entities.FromAssemblyContaining<ClassFromAssembly>();
        });
        
        fluently.AddEntity<YourSingleEntity>();
        
        fluently.AddOverrides(overrides => {
          overrides.FromAssemblyContaining<ClassFromAssembly>();
        });
        
        fluently.UseSqlServer();
      );
    });
}
```

## Basic usage in DbContext
```c#
public class MyContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    options.BuildModel(fluently => {
      fluently.AddEntities(x => x.WithBaseType<Entity>().FromAssemblyContaining<EntityOne>());
      fluently.UseSqlServer();
    });
  }
}
```

### Adding single entities
```c#
BuildModel(fluently => fluently
  .AddEntity<YourSingleEntity>()
  .AddEntity<YourOtherSingleEntity>());
```

### Adding and configuring single entities
```c#
BuildModel(fluently => fluently
  .AddEntity<YourSingleEntity>(x => x.Ignore(p => p.Prop)));
```

### Adding entities of specific base type from assembly
```c#
BuildModel(fluently =>
  fluently.AddEntities(x => {
    x.WithBaseType<BaseEntity>(); // Specify the base type
    x.FromAssemblyContaining<YourEntity>(); // Specify the assembly that contains the entity for scanning
  })
);
```

### Adding overrides for entity types from assembly (searches for ```IEntityTypeOverride<>``` implementations)
```c#
BuildModel(fluently =>
  fluently.AddOverrides(x => x.FromAssemblyContaining<YourOverride>());
);
```

### Adding database specific generation options
```c#
opts.UseSqlServer();
```

or

```c#
opts.UseSqlite();
```

## Creating your own conventions

* Implement `IModelBuilderConvention`:

```c#
public class MyConvention : IModelBuilderConvention
{
  public void Apply(ModelBuilder builder)
  {
    // Magic!
  }
}
```

* Add it to builder

```c#
BuildModel(fluently => {
  fluently.AddConvention<MyConvention>();
});
```

### Build status
[![Build status](https://ci.appveyor.com/api/projects/status/yccb8ad2msd26bad/branch/master?svg=true)](https://ci.appveyor.com/project/Grinderofl/fluentmodelbuilder/branch/master)
