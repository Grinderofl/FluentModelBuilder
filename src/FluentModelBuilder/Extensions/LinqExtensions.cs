using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentModelBuilder.Extensions
{
    public static class LinqExtensions
    {
        public static void Combine<T>(this IList<T> source, IEnumerable<T> second)
        {
            foreach (var item in second)
            {
                if(!source.Contains(item))
                    source.Add(item);
            }
        }
    }
}
