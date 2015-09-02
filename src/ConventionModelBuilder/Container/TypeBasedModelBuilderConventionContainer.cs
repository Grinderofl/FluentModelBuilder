using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;

namespace ConventionModelBuilder.Container
{
    public class TypeBasedModelBuilderConventionContainer : ModelBuilderConventionContainer
    {
        internal readonly Type Type;

        /// <exception cref="InvalidCastException">Type is not of type IModelBuilderConvention.</exception>
        public TypeBasedModelBuilderConventionContainer(Type type)
        {
            if(!typeof(IModelBuilderConvention).IsAssignableFrom(type))
                throw new InvalidCastException("Type is not of type IModelBuilderConvention");
            Type = type;
        }

        /// <exception cref="TargetInvocationException">The constructor being called throws an exception. </exception>
        /// <exception cref="MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to call this constructor. </exception>
        /// <exception cref="MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
        /// <exception cref="ArgumentNullException"><paramref name="type" /> is null. </exception>
        /// <exception cref="ArgumentException"><paramref name="type" /> is not a RuntimeType. -or-<paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true).</exception>
        /// <exception cref="NotSupportedException"><paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or- Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or-The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />. </exception>
        /// <exception cref="MissingMethodException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.No matching public constructor was found. </exception>
        /// <exception cref="TypeLoadException"><paramref name="type" /> is not a valid type. </exception>
        protected override void ApplyCore(ModelBuilder builder)
        {
            var instance = (IModelBuilderConvention) Activator.CreateInstance(Type);
            instance?.Apply(builder);
        }
    }
}