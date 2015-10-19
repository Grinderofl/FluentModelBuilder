using System.Linq;
using System.Reflection;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Helpers
{
    public class MethodHelper
    {
        public static MethodInfo EntityMethod =
            typeof (ModelBuilder).GetMethods().First(x => x.Name == "Entity" && x.IsGenericMethod);
    }
}
