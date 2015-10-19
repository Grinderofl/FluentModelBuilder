using System;
using System.Linq;
using System.Reflection;

namespace FluentModelBuilder.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool ImplementsInterfaceOfType(this Type type, Type interfaceType)
        {
            var interfaces = type.GetInterfaces();
            return interfaces.Any(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
        }
    }
}
