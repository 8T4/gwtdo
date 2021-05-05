using System;

namespace Gwtdo.Extensions
{
    /// <summary>
    /// Action extensions
    /// </summary>
    public static class ActionMappingMethods
    {
        /// <summary>
        /// Map Action
        /// </summary>
        /// <param name="phrase"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static (string, Action) MapAction(this string phrase, Action action)
        {
            return (phrase, action);
        }        
        
        /// <summary>
        /// Map Action
        /// </summary>
        /// <param name="phrase"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static (string, Action<T>) MapAction<T>(this string phrase, Action<T> action)
        {
            return (phrase, action);
        }        
    }
}