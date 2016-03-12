# Internals of Entity Framework Core

* To alter ModelBuilder outside DbContext, implement `IModelCustomizer` / inherit from 'ModelCustomizer' and replace in `IServiceCollection` after `AddEntityFramework()`
* To change when DbContext rebuilds model, inherit from 'ModelCacheKeyFactory', update it to generate a different key when model changes, and replace it in `IServiceCollection`.
