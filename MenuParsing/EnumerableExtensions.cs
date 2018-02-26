using System.Collections.Generic;
using System.Linq;

namespace MenuParsing
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> coll) => 
            coll == null || !coll.Any();
    }
}
