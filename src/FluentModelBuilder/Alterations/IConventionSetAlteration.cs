using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace FluentModelBuilder.Alterations
{
    /// <summary>
    /// Provides a way to alter the conventionset used to map the properties of scanned entities
    /// </summary>
    public interface IConventionSetAlteration
    {
        /// <summary>
        /// Alter the ConventionSet
        /// </summary>
        /// <param name="conventions">ConventionSet to alter</param>
        void Alter(ConventionSet conventions);
    }

    //public class ConventionAlteration : IConventionSetAlteration
    //{
    //    private readonly Assembly _assembly;

    //    public ConventionAlteration(Assembly assembly)
    //    {
    //        _assembly = assembly;
    //    }

    //    public void Alter(ConventionSet conventions)
    //    {
    //        //var types = _assembly.GetExportedTypes().Where(x => !x.GetTypeInfo().IsAbstract);

    //        var types = from type in _assembly.GetExportedTypes()
    //            where !type.GetTypeInfo().IsAbstract
    //            let entity = (from interfaceType in type.GetInterfaces()
    //                where
    //                    interfaceType.GetTypeInfo().IsGenericType &&
    //                    interfaceType.GetGenericTypeDefinition() == typeof (IConventionAlteration<>)
    //                select interfaceType.GetGenericArguments()[0]).FirstOrDefault()
    //            where entity != null
    //            select type;

    //        foreach (var type in types)
    //        {
    //            AlterIfMatches(x => (IList) x.BaseEntityTypeSetConventions, conventions, type);
    //        }
    //    }

    //    private void AlterIfMatches(Func<ConventionSet, IList> property, ConventionSet set, Type type)
    //    {
    //        
    //    }
        
    //}

    //public interface IConventionAlteration<TConvention>
    //{
    //    void Alter(IList<TConvention> convention);
    //}

    //public interface IEntityTypeAddedConventionAlteration : IConventionAlteration<IEntityTypeConvention>
    //{
    //}
}