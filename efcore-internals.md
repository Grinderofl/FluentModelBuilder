# Internals of Entity Framework Core

* To alter ModelBuilder outside DbContext, implement `IModelCustomizer` / inherit from `ModelCustomizer` and replace in `IServiceCollection` after `AddEntityFramework()`
* To change when DbContext rebuilds model, implement 'IModelCacheKeyFactory' / inherit from `ModelCacheKeyFactory` and generate a different key when model changes, and replace it in `IServiceCollection`.
* No more magic with DbContext creation
