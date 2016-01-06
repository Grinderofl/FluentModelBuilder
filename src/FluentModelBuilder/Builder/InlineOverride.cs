using System;

namespace FluentModelBuilder.Builder
{
    public class InlineOverride
    {
        public Type Type { get; }
        private readonly Action<object> _action;

        public InlineOverride(Type type, Action<object> action)
        {
            Type = type;
            _action = action;
        }

        public void Apply(object entityTypeBuilder)
        {
            _action(entityTypeBuilder);
        }
    }
}