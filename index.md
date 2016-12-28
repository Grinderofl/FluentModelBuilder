# Introducing FluentModelBuilder

## What is it
If you're writing an application with .NET Core using Entity Framework Core, you may want to have a better way of customising your backing model. `FluentModelBuilder` is a drop-in extension for doing exactly that in a fluent manner. It plugs itself into Entity Framework Core's own pipeline and even plays nice with dependency injection. There's a variety of extension points that allow configuring your model in a very conventional and automated way which should be very familiar if you come from Fluent NHibernate background.

## Installation
NuGet:
```c#
Install-Package FluentModelBuilder
```

If you want Relational Conventions:
```c#
Install-Package FluentModelBuilder.Relational
```

## How to use
To extend your pre-existing DbContext, just use `Configure` inside `AddDbContext<TContext>()`:

```c#
// Scan assembly of YourContext for entities

services.AddDbContext<YourContext>(opt => 
    opt
        .UseSqlServer("")
        .Configure(fluently => 
            fluently
                .Using(type => type.Namespace.EndsWith("Entities"))
                .AddEntitiesFromAssemblyOf<YourContext>()));
```

For more complex configuration, implement `IEntityAutoConfiguration` or subclass from `DefaultEntityAutoConfiguration`:

```c#
// Pick up all concrete types that inherit from Entity
public class MyEntityAutoConfiguration : DefaultEntityAutoConfiguration
{
    public override bool ShouldMap(Type type)
    {
        return base.ShouldMap(type) && type.GetTypeInfo().IsSubclassOf(typeof(Entity));
    }
}

// Configure service
services.AddDbContext<YourContext>(opt => 
    opt
        .UseSqlServer("")
        .Configure(fluently =>
            fluently
                .Using(new MyEntityAutoConfiguration())
                .AddEntitiesFromAssemblyOf<YourContext>()));
```

You can also use it in your DbContext's configure method. Example given using `From` entry point

```c#
// Inside DbContext
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.Configure(
        From.AssemblyOf<YourContext>(new MyEntityAutoConfiguration()));
}
```

If you want to put all your entity maps into separate files, implement `IEntityTypeOverride<T>` and add the assembly for overrides:

```c#
public class MyEntityOverride : IEntityTypeOverride<MyEntity>
{
    public void Override(EntityTypeBuilder<MyEntity> mapping)
    {
        mapping.ToTable("MyEntities");
    }
}

// From-syntax, without the Configure() bit

From
    .AssemblyOf<YourContext>(new MyEntityAutoConfiguration())
    .UseOverridesFromAssemblyOf<YourContext>();

```

### Dependency Injection

FluentModelBuilder adds a different way of altering your model and model builder programmatically using the application's service provider. When implementing interfaces `ITypeSource`, `IModelBuilderAlteration`, or `IModelBuilderConvention`, you can use constructor injection to retrieve dependencies. You'll also need to ensure that relevant Entity Framework services are added to your service provider.

```c#
// Alteration
public class MyAlteration : IAutoModelBuilderAlteration
{
    private IDependency _dependency;
    public MyAlteration(IDependency dependency)
    {
        _dependency = dependency;
    }

    public void Alter(AutoModelBuilder autoModelBuilder)
    {
        // .. Do something with dependency
    }
}

// Type Source
public class MyTypeSource : ITypeSource
{
    private IDependency _dependency;
    public MyTypeSource(IDependency dependency)
    {
        _dependency = dependency;
    }

    public IEnumerable<Type> GetTypes()
    {
        // .. do something with dependency
    }
}

// Convention
public class MyConvention : IModelBuilderConvention
{
    private IDependency _dependency;
    public MyConvention(IDependency dependency)
    {
        _dependency = dependency;
    }

    public void Apply(ModelBuilder modelBuilder)
    {
        // .. do something with dependency
    }
}

// Configure services
services
    .AddEntityFrameworkInMemoryDatabase()
    .AddDbContext<DbContext>((sp, x) => x.UseInMemoryDatabase().UseInternalServiceProvider(sp));
services.ConfigureEntityFramework(f => f.Using()
    .AddAlteration<MyAlteration>()
    .AddTypeSource<MyTypeSource>()
    .UseConvention<MyConvention>());
services.AddScoped<IDependency, MyDependency>();
```

`Alter()` will execute prior to `GetTypes()`, which will execute before `Apply()`, and all three of them will have access to the dependency. As DbContext is capable of creating its own scope, the dependencies can be singletons, transient, or scoped.

### Conventions
FluentModelBuilder has some degree of convention support. Currently there are following conventions:

* `DecimalPropertyConvention` - specifies the precision and scale of decimal types by default
* `PluralizingTableNameGeneratingConvention` - brings back automatically pluralized table names
* `IgnoreReadOnlyPropertiesConvention` - automatically ignores all read-only properties from being included in the DB mapping


```c#
From.AssemblyOf<YourContext>(new MyEntityConfiguration())
    .UseConvention(new DecimalPropertyConvention(5, 2))

    .UseConvention<PluralizingTableNameGeneratingConvention>();
```

You can also inherit from `AbstractEntityConvention` which applies itself to every entity inside the currently built model.

### Extensions

With EF Core 1.1.0, the ability to specify property accessor type to be a field was provided. FluentModelBuilder uses this to its full advantage providing an extension point over property configuration:

```c#
public class TestEntityOverride : IEntityTypeOverride<TestEntity>
{
    public void Override(EntityTypeBuilder<TestEntity> mapping)
    {
        mapping.Property(x => x.SomePropertyName).Access(PropertyAccessor.CamelCasePrefixField);
    }
}

public class TestEntity
{
    public string SomePropertyName { get { return _somePropertyName; } set { _somePropertyName = value; } }
    private string _somePropertyName;
}
```

Available accessor types:

* LowerCaseField
* LowerCasePrefixField
* CamelCaseField
* CamelCasePrefixField
* Custom

