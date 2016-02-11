using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Builder;

namespace FluentModelBuilder.Alterations
{
    public class ModelBuilderOverrideAlteration : IAutoModelBuilderAlteration
    {
        private readonly Assembly _assembly;

        public ModelBuilderOverrideAlteration(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Alter(AutoModelBuilder builder)
        {
            var types = from type in _assembly.GetExportedTypes()
                where !type.GetTypeInfo().IsAbstract &&
                      type == typeof (IModelBuilderOverride)
                select type;

            foreach (var type in types)
                builder.Override(Activator.CreateInstance(type) as IModelBuilderOverride);
        }
    }
}