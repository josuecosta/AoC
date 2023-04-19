namespace AoC17
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static List<T> Clone<T>(this List<T> list)
        {
            var clone = new List<T>();
            foreach (var item in list)
            {
                clone.Add(item);
            }
            return clone;
        }
    }
}
