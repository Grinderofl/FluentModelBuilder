using System.Linq;
using System.Reflection;
using Microsoft.Data.Entity;

namespace FluentModelBuilder
{
    public class ModelBuilderHelper
    {
        public static MethodInfo EntityMethod =
            typeof(ModelBuilder).GetMethods().First(x => x.Name == "Entity" && x.IsGenericMethod);
    }
}