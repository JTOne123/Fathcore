﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fathcore.Extensions
{
    /// <summary>
    /// Collection extensions.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the <see cref="List{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="func">The <see cref="Func{T, TResult}"/> delegate to perform on each element of the <see cref="List{T}"/>.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> func)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return Task.WhenAll(source.Select(func));
        }

        /// <summary>
        /// This method extends the LINQ methods to flatten a collection of items that have a property of children of the same type.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="childPropertySelector">Child property selector delegate of each item. IEnumerable'T' childPropertySelector(T itemBeingFlattened).</param>
        /// <returns>Returns a one level list of elements of type T.</returns>
        public static IEnumerable<T> FlattenList<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childPropertySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (childPropertySelector == null)
                throw new ArgumentNullException(nameof(childPropertySelector));

            return source
                .FlattenList((itemBeingFlattened, objectsBeingFlattened) =>
                    childPropertySelector(itemBeingFlattened))
                .ToList();
        }

        /// <summary>
        /// This method extends the LINQ methods to flatten a collection of items that have a property of children of the same type.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="childPropertySelector">Child property selector delegate of each item. IEnumerable'T' childPropertySelector (T itemBeingFlattened, IEnumerable'T' objectsBeingFlattened).</param>
        /// <returns>Returns a one level list of elements of type T.</returns>
        private static IEnumerable<T> FlattenList<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>, IEnumerable<T>> childPropertySelector)
        {
            return source
                .Concat(source
                    .Where(item => childPropertySelector(item, source) != null)
                    .SelectMany(itemBeingFlattened =>
                        FlattenList(childPropertySelector(itemBeingFlattened, source), childPropertySelector)));
        }

        /// <summary>
        /// Generates tree of items from item list.
        /// </summary>
        /// <typeparam name="T">Type of item in collection.</typeparam>
        /// <typeparam name="U">Type of parent id.</typeparam>
        /// <param name="source">Collection of items.</param>
        /// <param name="idPropertySelector">Function extracting item's id.</param>
        /// <param name="parentIdPropertySelector">Function extracting item's parent_id.</param>
        /// <param name="childPropertySelector">Child property selector delegate of each item.</param>
        /// <param name="rootId">Root element id.</param>
        /// <returns>Tree of items</returns>
        public static IEnumerable<T> UnFlattenList<T, U>(this IEnumerable<T> source, Func<T, U> idPropertySelector, Func<T, U> parentIdPropertySelector, Expression<Func<T, IEnumerable<T>>> childPropertySelector, U rootId = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (idPropertySelector == null)
                throw new ArgumentNullException(nameof(idPropertySelector));

            if (parentIdPropertySelector == null)
                throw new ArgumentNullException(nameof(parentIdPropertySelector));

            if (childPropertySelector == null)
                throw new ArgumentNullException(nameof(childPropertySelector));

            return source.UnFlattenListIterator(idPropertySelector, parentIdPropertySelector, childPropertySelector, rootId);
        }

        /// <summary>
        /// Generates tree of items from item list.
        /// </summary>
        /// <typeparam name="T">Type of item in collection.</typeparam>
        /// <typeparam name="U">Type of parent id.</typeparam>
        /// <param name="source">Collection of items.</param>
        /// <param name="idPropertySelector">Function extracting item's id.</param>
        /// <param name="parentIdPropertySelector">Function extracting item's parent_id.</param>
        /// <param name="childPropertySelector">Child property selector delegate of each item.</param>
        /// <param name="rootId">Root element id.</param>
        /// <returns>Tree of items</returns>
        private static IEnumerable<T> UnFlattenListIterator<T, U>(this IEnumerable<T> source, Func<T, U> idPropertySelector, Func<T, U> parentIdPropertySelector, Expression<Func<T, IEnumerable<T>>> childPropertySelector, U rootId)
        {
            foreach (var item in source.Where(c => parentIdPropertySelector(c).Equals(rootId)))
            {
                yield return item.SetPropertyValue(childPropertySelector, source.UnFlattenList(idPropertySelector, parentIdPropertySelector, childPropertySelector, idPropertySelector(item)).ToList());
            }
        }
    }
}