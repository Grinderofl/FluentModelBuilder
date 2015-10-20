using System.Linq;
using System.Reflection;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core.Helpers
{
    public class MethodHelper
    {
        /// <summary>
        /// Retrieves the generic Entity`1[TEntity] method from ModelBuilder
        /// </summary>
        public static MethodInfo EntityMethod =
            typeof (ModelBuilder).GetMethods().First(x => x.Name == "Entity" && x.IsGenericMethod);
    }
}
