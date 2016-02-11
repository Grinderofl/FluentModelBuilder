using System.Linq;
using System.Reflection;
using FluentModelBuilder.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Alterations
{
    public class EntityTypeOverrideAlteration : IAutoModelBuilderAlteration
    {
        private readonly Assembly _assembly;

        public EntityTypeOverrideAlteration(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Alter(AutoModelBuilder builder)
        {
            var types = from type in _assembly.GetExportedTypes()
                where !type.GetTypeInfo().IsAbstract
                let entity = (from interfaceType in type.GetInterfaces()
                    where
                        interfaceType.GetTypeInfo().IsGenericType &&
                        interfaceType.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>)
                    select interfaceType.GetGenericArguments()[0]).FirstOrDefault()
                where entity != null
                select type;

            foreach(var type in types)
                builder.Override(type);
        }
    }
}