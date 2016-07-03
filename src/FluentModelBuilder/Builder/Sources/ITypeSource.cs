using System;
using System.Collections.Generic;

namespace FluentModelBuilder.Builder.Sources
{
    public interface ITypeSource
    {
        IEnumerable<Type> GetTypes();
    }
}