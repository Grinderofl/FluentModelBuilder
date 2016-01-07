using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using FluentModelBuilder.Alterations;

namespace FluentModelBuilder.Core
{
    public class AlterationCollectionBase<T, TAlteration> : IEnumerable<TAlteration> where T : AlterationCollectionBase<T, TAlteration>
    {
        protected readonly List<TAlteration> Alterations = new List<TAlteration>();

        private void Add(Type type)
        {
            Add((TAlteration)Activator.CreateInstance(type));
        }

        /// <summary>
        /// Creates an instance of TAlteration from a generic type parameter and adds it to alterations collection
        /// </summary>
        /// <typeparam name="TAlterationType">Type of TAlteration</typeparam>
        /// <returns>T</returns>
        public T Add<TAlterationType>() where TAlterationType : TAlteration
        {
            Add(typeof (TAlterationType));
            return (T) this;
        }

        /// <summary>
        /// Adds an alteration
        /// </summary>
        /// <param name="alteration">Alteration to add</param>
        /// <returns>T</returns>
        public T Add(TAlteration alteration)
        {
            if(!Alterations.Exists(a => a.GetType() == alteration.GetType() && alteration.GetType() != typeof(EntityTypeOverrideAlteration)))
                Alterations.Add(alteration);
            return (T) this;
        }

        /// <summary>
        /// Adds all alterations from specified assembly
        /// </summary>
        /// <param name="assembly">Assembly to use</param>
        /// <returns>T</returns>
        public T AddFromAssembly(Assembly assembly)
        {
            foreach(var type in assembly.GetExportedTypes())
                if (typeof (TAlteration).IsAssignableFrom(type))
                    Add(type);
            return (T) this;
        }

        /// <summary>
        /// Adds all alterations from assembly which contains the specified type
        /// </summary>
        /// <typeparam name="TAssemblyType">Type contained in required assembly</typeparam>
        /// <returns>T</returns>
        public T AddFromAssemblyOf<TAssemblyType>()
        {
            return AddFromAssembly(typeof(TAssemblyType).GetTypeInfo().Assembly);
        }

        public IEnumerator<TAlteration> GetEnumerator()
        {
            return Alterations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
    }
}