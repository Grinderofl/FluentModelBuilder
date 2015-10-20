using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Core.Extensions;
using FluentModelBuilder.Core.Helpers;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core.Contributors.Impl
{
    public class SingleTypeOverrideContributor : IOverrideContributor
    {
        private readonly Type _type;

        public SingleTypeOverrideContributor(Type type)
        {
            if(!type.ImplementsInterfaceOfType(typeof(IEntityTypeOverride<>)))
                throw new ArgumentException("Type does not implement IEntityTypeOverride<>");
            _type = type;
        }

        public void Contribute(ModelBuilder modelBuilder)
        {
            var method = _type.GetMethod("Configure");
            var target =
                _type.GetInterfaces()
                    .Single(x => x.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>))
                    .GenericTypeArguments.First();

            var entity = modelBuilder.GenericEntity(target);
            var overrideInstance = Activator.CreateInstance(_type);
            method.Invoke(overrideInstance, new[] {entity});
        }
    }
}