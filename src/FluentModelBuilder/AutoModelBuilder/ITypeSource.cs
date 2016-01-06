using System;
using System.Collections.Generic;

namespace FluentModelBuilder
{
    public interface ITypeSource
    {
        IEnumerable<Type> GetTypes();
        string GetIdentifier();
    }
}