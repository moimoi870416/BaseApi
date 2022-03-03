using System.Collections.Generic;
using System.Linq;

namespace Base.Api.Helper
{
    public static class ListHelper
    {
        public static bool IsListNullOrEmpty<T>(this List<T> list)
        {
            return list == null || !list.Any();
        }
    }
}