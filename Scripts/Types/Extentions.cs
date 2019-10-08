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

        public static TResult Bind<TInput, TResult>(this TInput obj, Func<TInput, TResult> func)
        {
            return func(obj);
        }

        public static TResult With<TInput, TResult>(this TInput obj, Func<TInput, TResult> selector)
        where TInput : class
        where TResult : class
        {
            if (obj.Equals(null)) return null;
            return selector(obj);
        }

        public static TResult Return<TInput, TResult>(this TInput obj, Func<TInput, TResult> selector, TResult failureValue = default)
        where TInput : class
        {
            if (obj == null) return failureValue;
            return selector(obj);
        }

        public static TInput If<TInput>(this TInput obj, Predicate<TInput> predicate)
        where TInput : class
        {
            if (obj == null) return null;
            return predicate(obj) ? obj : null;
        }

        public static TInput Do<TInput>(this TInput obj, Action<TInput> action)
        where TInput : class
        {
            if (obj == null) return null;
            action(obj);
            return obj;
        }
    }
}