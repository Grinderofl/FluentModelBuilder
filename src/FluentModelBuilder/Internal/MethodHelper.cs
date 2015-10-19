using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Internal
{
    public class MethodHelper
    {
        public static MethodInfo EntityMethod =
            typeof (ModelBuilder).GetMethods().First(x => x.Name == "Entity" && x.IsGenericMethod);
    }
}
