using System.Linq;
using System.Reflection;
using FluentModelBuilder.Builder;

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
            var types = from type in _assembly.ExportedTypes
                where !type.GetTypeInfo().IsAbstract
                let entity = (from interfaceType in type.GetTypeInfo().ImplementedInterfaces
                    where
                        interfaceType.GetTypeInfo().IsGenericType &&
                        interfaceType.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>)
                    select interfaceType.GenericTypeArguments[0]).FirstOrDefault()
                where entity != null
                select type;

            foreach (var type in types)
                builder.UseOverride(type);
        }
    }
}