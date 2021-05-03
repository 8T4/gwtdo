using System;

namespace Gwtdo.Extensions
{
    public static class ActionMethods
    {
        public static (string, Action) MapAction(this string phrase, Action action)
        {
            return (phrase, action);
        }        
        
        public static (string, Action<T>) MapAction<T>(this string phrase, Action<T> action)
        {
            return (phrase, action);
        }        
    }
}