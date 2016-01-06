using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.Metadata.Internal;

namespace FluentModelBuilder
{
    public class FluentModelBuilderConvention : IModelConvention
    {
        private readonly FluentModelBuilderConfiguration _configuration;

        public FluentModelBuilderConvention(FluentModelBuilderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public InternalModelBuilder Apply(InternalModelBuilder modelBuilder)
        {
            _configuration.Apply(modelBuilder);
            //foreach (var alteration in _configuration.ModelBuilderAlterations)
            //{
            //    var internalBuilder = modelBuilder.Entity(alteration.Type, ConfigurationSource.Explicit);
            //    // internalBuilder = InternalEntityTypeBuilder
            //    var configuration = typeof (EntityTypeBuilder<>).MakeGenericType(alteration.Type);
            //    // configuration = EntityTypeBuilder<Entity>
            //    var instance = Activator.CreateInstance(configuration, internalBuilder);
            //    // instance = new EntityTypeBuilder<Entity>(internalBuilder)

            //    var overrideMethod = typeof (FluentModelBuilderConvention).GetMethod("OverrideHelper",
            //        BindingFlags.NonPublic | BindingFlags.Instance);

            //    // overrideMethod = OverrideHelper<T>(ETB<T>, IETO<T>)

            //    var overrideInstance = Activator.CreateInstance(alteration.AlterationType);

            //    overrideMethod.MakeGenericMethod(alteration.Type)
            //        .Invoke(this, new[] {instance, overrideInstance});
            //}
            
            
            return modelBuilder;
        }

        //private void OverrideHelper<T>(EntityTypeBuilder<T> configuration, IEntityTypeOverride<T> mappingOverride) where T : class
        //{
        //    mappingOverride.Override(configuration);
        //}
    }
}