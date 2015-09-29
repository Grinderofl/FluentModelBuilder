using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConventionModelBuilder.Conventions.Options;
using ConventionModelBuilder.Conventions.Overrides;
using Microsoft.Data.Entity;

namespace ConventionModelBuilder.Conventions
{
    public class EntityTypeOverrideDiscoveryConvention : IModelBuilderConvention
    {
        public EntityTypeOverrideDiscoveryConventionOptions Options { get; } = new EntityTypeOverrideDiscoveryConventionOptions();
        public void Apply(ModelBuilder builder)
        {
            var types = Options.Assemblies.SelectMany(x => x.GetExportedTypes());
            types =
                types.Where(
                    x =>
                        x.GetInterfaces()
                            .Any(
                                c =>
                                    c.GetTypeInfo().IsGenericType &&
                                    c.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>)));

            var entityMethod = typeof (ModelBuilder).GetMethods().First(x => x.Name == "Entity" && x.IsGenericMethod);
            foreach (var type in types)
            {
                var method = type.GetMethod("Configure");
                var target =
                    type.GetInterfaces()
                        .Single(x => x.GetGenericTypeDefinition() == typeof (IEntityTypeOverride<>))
                        .GenericTypeArguments.First();
                var invokedEntity = entityMethod.MakeGenericMethod(target).Invoke(builder, new object[] {});
                //var entity = builder.Entity(type);
                var obj = Activator.CreateInstance(type);
                method.Invoke(obj, new object[] {invokedEntity});

            }
        }
    }
}
