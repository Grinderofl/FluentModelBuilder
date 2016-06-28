using System;

namespace FluentModelBuilder.Builder
{
    public class InlineOverride
    {
        private readonly Action<object> _action;

        public InlineOverride(Type type, Action<object> action)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            Type = type;
            _action = action;
        }

        public Type Type { get; }

        public void Apply(object entityTypeBuilder)
        {
            _action(entityTypeBuilder);
        }
    }
}