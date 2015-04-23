// -----------------------------------------------------------------------
// <copyright file="InlineCountCollection{T}.cs" company="Project Contributors">
// Copyright 2012 - 2015 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData
{
    using System.Collections.Generic;

    /// <summary>
    /// A class which is used to return results with an inline count.
    /// </summary>
    /// <typeparam name="T">The type of item contained in the collection.</typeparam>
    [System.Obsolete("This class has been replaced by the InlineCount<T> class and will be removed in a future release.")]
    public sealed class InlineCountCollection<T> : IEnumerable<T>
    {
        private readonly IList<T> items;

        /// <summary>
        /// Initialises a new instance of the <see cref="InlineCountCollection{T}"/> class.
        /// </summary>
        /// <param name="items">The items to be returned.</param>
        /// <param name="count">The total result count.</param>
        public InlineCountCollection(IList<T> items, int count)
        {
            this.items = items;
            this.Count = count;
        }

        /// <summary>
        /// Gets or sets the total result count.
        /// </summary>
        public int Count
        {
            get;
            set;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}