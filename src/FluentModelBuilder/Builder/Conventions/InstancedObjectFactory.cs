using System;

namespace FluentModelBuilder.Builder.Conventions
{
    public class InstancedObjectFactory<T> : AbstractObjectFactory<T>
    {
        private readonly T _instance;

        public InstancedObjectFactory(T instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            _instance = instance;
        }

        public override T Create(IServiceProvider serviceProvider) => _instance;
    }
}