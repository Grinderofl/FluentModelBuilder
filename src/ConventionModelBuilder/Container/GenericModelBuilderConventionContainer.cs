using System;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;

namespace ConventionModelBuilder.Container
{
    public class GenericModelBuilderConventionContainer<T> : ModelBuilderConventionContainer where T : class, IModelBuilderConvention
    {
        /// <exception cref="MissingMethodException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.The type that is specified for <paramref name="T" /> does not have a parameterless constructor. </exception>
        protected override void ApplyCore(ModelBuilder builder)
        {
            var instance = Activator.CreateInstance<T>();
            instance?.Apply(builder);
        }
    }
}