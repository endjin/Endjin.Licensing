namespace Endjin.Licensing.Extensions
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public static class TraversalExtensions
    {
        /// <summary>
        /// Execute an action for each item in the enumerable
        /// </summary>
        /// <param name="items">
        /// The items over which to execute the action and its zero-based index
        /// </param>
        /// <param name="action">
        /// The action to carry out
        /// </param>
        /// <typeparam name="T">
        /// The type of the items in the enumeration
        /// </typeparam>
        public static void ForEachFailEnd<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
            {
                throw new NullReferenceException();
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            var exceptions = new List<Exception>();

            foreach (T item in items)
            {
                try
                {
                    action(item);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TraversalExtensions.cs" company="Endjin Ltd">
//   Copyright © 2015 Endjin Ltd
// </copyright>
// --------------------------------------------------------------------------------------------------------------------