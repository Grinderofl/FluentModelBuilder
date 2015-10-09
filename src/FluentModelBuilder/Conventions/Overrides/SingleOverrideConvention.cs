using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions.Overrides
{
    public class SingleOverrideConvention<T> : IModelBuilderConvention
    {
        public SingleOverrideConvention()
        {
            if(!typeof(T).ImplementsInterfaceOfType(typeof(IEntityTypeOverride<>)))
                throw new InvalidOperationException($"{nameof(T)} is not of type IEntityTypeOverride<>");
        }

        private T _instance;

        public SingleOverrideConvention(T instance) : this()
        {
            _instance = instance;
        }

        protected T GetInstance()
        {
            if (_instance == null)
                _instance = Activator.CreateInstance<T>();

            return _instance;
        }

        public virtual void Apply(ModelBuilder builder)
        {
            var method = typeof (T).GetMethod("Configure");

            var target = typeof (T).GetInterfaces()
                .Single(x => x.GetGenericTypeDefinition() == typeof(IEntityTypeOverride<>))
                .GenericTypeArguments.First();

            var entity = ModelBuilderHelper.EntityMethod.MakeGenericMethod(target)
                .Invoke(builder, new object[] {});

            method.Invoke(GetInstance(), new[] {entity});
        }
    }
}