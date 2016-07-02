using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Conventions;
using FluentModelBuilder.Extensions;

namespace FluentModelBuilder.Alterations
{
    public class ModelBuilderConventionAlteration : IAutoModelBuilderAlteration
    {
        private readonly Assembly _assembly;

        public ModelBuilderConventionAlteration(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Alter(AutoModelBuilder builder)
        {
            var types = from type in _assembly.GetExportedTypes()
                where !type.GetTypeInfo().IsAbstract &&
                      type.ClosesInterface(typeof(IModelBuilderConvention))
                select type;

            foreach (var type in types)
                builder.UseConvention(Activator.CreateInstance(type) as IModelBuilderConvention);
        }
    }
}