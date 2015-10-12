using System.Collections.Generic;

namespace FluentModelBuilder.v2
{
    public static class AssemblyExtensions
    {
        public static void AddIfNotExists<T>(this IList<T> list, T item)
        {
            if(!list.Contains(item))
                list.Add(item);
        }
    }
}