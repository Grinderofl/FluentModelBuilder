# FluentModelBuilder

Alternative for creating DbContext: using fluent interface and possible conventions

# Basic Usage

```c#
public void Configure(IServiceCollection services)
{
  services.AddEntityFramework()
    .AddSqlServer()
    .AddDbContext<DbContext>(dbContextOptions => {
    
      dbContextOptions.UseSqlServer(...);
      
      dbContextOptions.BuildModel(opts =>
      
        opts.AddEntities(entities => {
          entities.WithBaseType<EntityBase>();
          entities.FromAssemblyContaining<ClassFromAssembly>();
        });
        
        opts.AddEntity<YourSingleEntity>();
        
        opts.AddOverrides(overrides => {
          overrides.FromAssemblyContaining<ClassFromAssembly>();
        });
        
        opts.UseSqlServer();
      );
    });
}
```

### Adding single entities:
```c#
BuildModel(opts => opts
  .AddEntity<YourSingleEntity>()
  .AddEntity<YourOtherSingleEntity>());
```

### Adding and configuring single entities
```c#
BuildModel(opts => opts
  .AddEntity<YourSingleEntity>(x => x.Ignore(p => p.Prop)));
```

### To enable generation of proper database specific identities, add
```c#
opts.UseSqlServer();
```

or

```c#
opts.UseSqlite();
```
