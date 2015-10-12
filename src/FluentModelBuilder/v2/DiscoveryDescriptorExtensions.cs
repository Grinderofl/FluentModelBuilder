using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Conventions.Core.Criteria;

namespace FluentModelBuilder.v2
{
    public static class DiscoveryDescriptorExtensions
    {
        public static DiscoveryOptions FromSharedAssemblies(this DiscoveryOptions descriptor)
        {
            descriptor.UseSharedAssemblies = true;
            return descriptor;
        }

        public static DiscoveryOptions WithBaseType<T>(this DiscoveryOptions options)
        {
            return options.WithBaseType(typeof(T));
        }

        public static DiscoveryOptions WithBaseType(this DiscoveryOptions options, Type type)
        {
            if(!options.Criterias.Any(x => x is NonAbstractCriteria))
                options.Criterias.Add(new NonAbstractCriteria());
            options.Criterias.Add(new BaseTypeCriteria(type));
            return options;
        }

        public static DiscoveryOptions When(this DiscoveryOptions options, Func<TypeInfo, bool> criteria)
        {
            options.Criterias.Add(new ExpressionCriteria(criteria));
            return options;
        }

        public static DiscoveryOptions Namespace(this DiscoveryOptions options, Func<string, bool> namespaceAction)
        {
            return options.When(x => namespaceAction(x.Namespace));
        }
    }
}