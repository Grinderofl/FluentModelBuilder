using System;
using FluentModelBuilder.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Builder.Conventions
{
    public class TypeBasedObjectFactory<T> : AbstractObjectFactory<T> where T : class
    {
        private readonly Type _type;

        public TypeBasedObjectFactory(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!type.IsSubclassOf(typeof(T)) && !type.ClosesInterface(typeof(T)))
                throw new ArgumentException($"{nameof(type)} is not of type {typeof(T)}");
            _type = type;
        }

        public override T Create(IServiceProvider serviceProvider)
            => ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, _type) as T;
    }
}