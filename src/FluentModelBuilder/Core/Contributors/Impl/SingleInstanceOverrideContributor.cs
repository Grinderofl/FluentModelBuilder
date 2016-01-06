using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core.Contributors.Impl
{
    public class SingleInstanceOverrideContributor<TEntity> : IOverrideContributor where TEntity : class
    {
        private readonly IEntityTypeOverride<TEntity> _instance;

        public SingleInstanceOverrideContributor(IEntityTypeOverride<TEntity> instance)
        {
            _instance = instance;
        }

        public void Contribute(ModelBuilder modelBuilder)
        {
            _instance.Override(modelBuilder.Entity<TEntity>());
        }
    }
}