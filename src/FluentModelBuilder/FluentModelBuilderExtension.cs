using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder
{
    public class FluentModelBuilderExtension : IDbContextOptionsExtension
    {
        public FluentModelBuilderExtension()
        {
            Entities = new EntitiesBuilder();
            Assemblies = new AssembliesBuilder();
            Overrides = new OverridesBuilder();
        }

        public FluentModelBuilderExtension(FluentModelBuilderExtension copyFrom)
        {
            Entities = copyFrom.Entities;
            Extension = copyFrom.Extension;
            Assemblies = copyFrom.Assemblies;
            Overrides = copyFrom.Overrides;
        }

        public EntitiesBuilder Entities { get; }
        public IBuilderExtension Extension { get; set; }
        public AssembliesBuilder Assemblies { get; } 
        public OverridesBuilder Overrides { get; }

        public void ApplyServices(EntityFrameworkServicesBuilder builder)
        { 
            Extension.Apply(builder);
        }
    }
}