using System;
using System.Linq;
using System.Reflection;

namespace FluentModelBuilder.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks whether type implements interface
        /// </summary>
        /// <param name="type">Type to check for implementation</param>
        /// <param name="interfaceType">Interface type</param>
        /// <returns>True if type implements interface, false if not.</returns>
        public static bool ImplementsInterfaceOfType(this Type type, Type interfaceType)
        {
            var interfaces = type.GetInterfaces();
            return interfaces.Any(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
        }
    }
}