using System;
using System.Collections.Generic;

namespace FluentModelBuilder.AutoModelBuilder.Sources
{
    public interface ITypeSource
    {
        IEnumerable<Type> GetTypes();
        string GetIdentifier();
    }
}