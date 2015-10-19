using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Internal;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors.Internal
{
    public class SingleOverrideContributor : IOverrideContributor
    {
        private readonly Type _type;

        public SingleOverrideContributor(Type type)
        {
            if(!typeof(IEntityTypeOverride<>).IsAssignableFrom(type))
                throw new ArgumentException("Type does not implement IEntityTypeOverride<>");
            _type = type;
        }

        public void Contribute(ModelBuilder modelBuilder)
        {
            var method = _type.GetMethod("Configure");
            var target =
                _type.GetInterfaces()
                    .Single(x => x.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>))
                    .GenericTypeArguments.First();

            var entity = MethodHelper.EntityMethod.MakeGenericMethod(target).Invoke(modelBuilder, new object[] {});
            var overrideInstance = Activator.CreateInstance(_type);
            method.Invoke(overrideInstance, new[] {entity});
        }
    }

    public class SingleOverrideContributor<TEntity> : IOverrideContributor where TEntity : class
    {
        private readonly IEntityTypeOverride<TEntity> _instance;

        public SingleOverrideContributor(IEntityTypeOverride<TEntity> instance)
        {
            _instance = instance;
        }

        public void Contribute(ModelBuilder modelBuilder)
        {
            _instance.Configure(modelBuilder.Entity<TEntity>());
        }
    }
}