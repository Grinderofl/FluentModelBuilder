using System;
using System.Collections.Generic;

namespace FluentModelBuilder.v2
{
    public class SingleTypeSource : ITypeSource
    {
        private readonly Type _type;

        public SingleTypeSource(Type type)
        {
            _type = type;
        }

        public IEnumerable<Type> GetTypes()
        {
            yield return _type;
        }
    }
}