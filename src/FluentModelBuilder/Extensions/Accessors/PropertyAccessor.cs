using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.Extensions.Accessors
{
    /// <summary>
    /// Provides access type for particular property
    /// </summary>
    public abstract class PropertyAccessor
    {
        /// <summary>
        /// Exposes the property to be accessed as "lowercasefield"
        /// </summary>
        public static PropertyAccessor LowerCaseField = new LowerCaseFieldPropertyAccessor();

        /// <summary>
        /// Exposes the property to be accessed as "_lowercasefield"
        /// </summary>
        public static PropertyAccessor LowerCasePrefixField = new LowerCasePrefixFieldPropertyAccessor();
        
        /// <summary>
        /// Exposes the property to be accessed as "camelCaseField"
        /// </summary>
        public static PropertyAccessor CamelCaseField = new CamelCaseFieldPropertyAccessor();

        /// <summary>
        /// Exposes the property to be accessed as "_camelCaseField" 
        /// </summary>
        public static PropertyAccessor CamelCasePrefixField = new CamelCasePrefixFieldPropertyAccessor();
        
        /// <summary>
        /// Exposes the property to be accessed via provided value
        /// </summary>
        /// <param name="fieldName">Field name to use</param>
        /// <returns>PropertyAccessor</returns>
        public static PropertyAccessor Custom(string fieldName) => new CustomNameFieldPropertyAccessor(fieldName);

        private readonly PropertyAccessMode _propetyAccessMode;

        protected PropertyAccessor(PropertyAccessMode propetyAccessMode)
        {
            _propetyAccessMode = propetyAccessMode;
        }

        public virtual void Modify<T>(PropertyBuilder<T> entry) where T : class
        {
            SetPropertyAccessMode(entry);
        }

        protected virtual void SetPropertyAccessMode<T>(PropertyBuilder<T> entry) where T : class
        {
            entry.UsePropertyAccessMode(_propetyAccessMode);
        }
    }
}