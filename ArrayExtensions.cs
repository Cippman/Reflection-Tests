using UnityEngine;

namespace CippSharp.Reflection.Extensions
{
    public static class ArrayExtensions
    {
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length < 1;
        }

    }
}