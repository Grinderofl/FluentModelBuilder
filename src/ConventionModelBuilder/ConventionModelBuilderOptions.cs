using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ConventionModelBuilder.Container;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Framework.DependencyInjection.Extensions;
using JetBrains.Annotations;

namespace ConventionModelBuilder
{
    public class ConventionModelBuilderOptions
    {
        public ConventionModelBuilderOptions()
        {
            ModelBuilderConventions = new Collection<ModelBuilderConventionContainer>();
        }

        public static ICoreConventionSetBuilder CreateDefaultConventions()
        {
            return new CoreConventionSetBuilder();
        }

        public ConventionModelBuilderOptions CoreConvention(ICoreConventionSetBuilder coreConventions)
        {
            CoreConventionSetBuilder = coreConventions;
            return this;
        }

        public ICoreConventionSetBuilder CoreConventionSetBuilder { get; set; }

        public ICollection<ModelBuilderConventionContainer> ModelBuilderConventions { get; }

        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public ConventionModelBuilderOptions UseConvention(IModelBuilderConvention convention)
        {
            if(FindContainer<InstancedModelBuilderConventionContainer>(x => x.Instance.GetType() == convention.GetType()) == null)
                ModelBuilderConventions.Add(new InstancedModelBuilderConventionContainer(convention));
            return this;
        }

        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public ConventionModelBuilderOptions UseConvention<T>() where T : class, IModelBuilderConvention
        {
            if (!IsRegistered<GenericModelBuilderConventionContainer<T>>())
                ModelBuilderConventions.Add(new GenericModelBuilderConventionContainer<T>());
            return this;
        }

        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        protected ModelBuilderConventionContainer FindContainer<T>([CanBeNull] Func<T, bool> condition = null) where T : ModelBuilderConventionContainer
        {
            var items = ModelBuilderConventions.Where(x => x is T);
            if(condition != null)
                return ModelBuilderConventions.FirstOrDefault(x => x is T && condition((T) x));

            return items.FirstOrDefault();
        }

        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        protected bool IsRegistered<T>(Func<T, bool> condition = null) where T : ModelBuilderConventionContainer
        {
            return FindContainer(condition) != null;
        }
    }
}