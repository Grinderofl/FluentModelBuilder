using System;
using FluentModelBuilder.Core.Helpers;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Retrieves the Generic EntityTypeBuilder`1 via Entity`1 generic method
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static object GenericEntity(this ModelBuilder modelBuilder, Type entityType)
        {
            return MethodHelper.EntityMethod.MakeGenericMethod(entityType).Invoke(modelBuilder, new object[] {});
        }
    }
}