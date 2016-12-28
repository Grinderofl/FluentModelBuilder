using System;
using System.Collections.Generic;
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
                   type.GetTypeInfo().GenericTypeArguments.Length > 0;
        }

        public static bool ClosesInterface(this Type type, Type interfaceType)
        {
            return
                type.GetTypeInfo().ImplementedInterfaces
                    .Any(
                        x =>
                        x == interfaceType ||
                            (x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == interfaceType));
        }

        public static bool IsSubclassOf(this Type type, Type superClassType)
        {
            return type.GetTypeInfo().IsSubclassOf(superClassType);
        }

        public static bool IsDbContextType(this Type type)
        {
            return typeof(DbContext).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

        public static IEnumerable<Type> GetTypesImplementingInterface(this Assembly assembly, Type interfaceType)
        {
            return
                assembly.ExportedTypes.Where(x => x.ClosesInterface(interfaceType));
        }
        
    }
}