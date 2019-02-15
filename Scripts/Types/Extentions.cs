using UnityEngine;

namespace SpaceEngine
{
    static class Extentions
    {
        public static bool IsOneOf(this StarClass starClass, params StarClass[] allowedClasses)
        {
            foreach (var allowed in allowedClasses)
            {
                if (starClass == allowed) return true;
            }
            return false;
        }

    }
}
