using System;
using UnityEngine;

namespace SpaceEngine
{
    static class Extentions
    {
        public static bool IsOneOf(this StarClass starClass, params StarClass[] starClasses)
        {
            foreach (var oneOf in starClasses)
            {
                if (starClass == oneOf) return true;
            }
            return false;
        }
    }
}