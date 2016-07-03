using System;

namespace FluentModelBuilder.Builder.Conventions
{
    public abstract class AbstractObjectFactory<T> : IObjectFactory<T>
    {
        public abstract T Create(IServiceProvider serviceProvider);
    }
}
