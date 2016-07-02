using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Alterations;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsEntityTypeOverrideType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IEntityTypeOverride<>) &&
                   type.GetGenericArguments().Length > 0;
        }

        public static bool ClosesInterface(this Type type, Type interfaceType)
        {
            return
                type.GetInterfaces()
                    .Any(
                        x =>
                        x == interfaceType ||
                            (x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == interfaceType));
        }

        public static bool IsDbContextType(this Type type)
        {
            return typeof(DbContext).IsAssignableFrom(type);
        }
    }
}