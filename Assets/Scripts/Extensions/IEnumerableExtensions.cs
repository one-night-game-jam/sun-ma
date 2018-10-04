using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IEnumerableExtensions
{
    public static Vector2 Sum(this IEnumerable<Vector2> source)
    {
        return source.Aggregate((a, b) => a + b);
    }
}
