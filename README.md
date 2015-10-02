# ConventionModelBuilder

Alternative for creating DbContext manually in EF7, by using conventions instead

# Usage

```c#
public void Configure(IServiceCollection services)
{
  services.AddEntityFramework()
    .AddSqlServer()
    .AddDbContext<DbContext>(dbContextOptions => {
    
      dbContextOptions.UseSqlServer(...);
      
      dbContextOptions.BuildModelUsingConventions(opts =>
      
        opts.AddEntities(entities => {
          entities.WithBaseType<EntityBase>();
          entities.FromAssemblyContaining<ClassFromAssembly>();
        });
        
        opts.AddOverrides(overrides => {
          overrides.FromAssemblyContaining<ClassFromAssembly>();
        });
        
        opts.UseSqlServer();
      );
    });
}
```
