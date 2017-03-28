// -----------------------------------------------------------------------
// <copyright file="PagedResult{T}.cs" company="Project Contributors">
// Copyright 2012 - 2017 Project Contributors
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
    /// A class which is used to return paged results with an overall result count.
    /// </summary>
    /// <typeparam name="T">The type of item contained in the collection.</typeparam>
    [System.Runtime.Serialization.DataContract]
    public sealed class PagedResult<T>
    {
        [System.Runtime.Serialization.DataMember(Name = "odata.count", Order = 0)]
        private readonly int count;

        [System.Runtime.Serialization.DataMember(Name = "value", Order = 1)]
        private readonly IList<T> results;

        /// <summary>
        /// Initialises a new instance of the <see cref="PagedResult{T}"/> class.
        /// </summary>
        /// <param name="results">The items to be returned.</param>
        /// <param name="count">The total result count.</param>
        public PagedResult(IList<T> results, int count)
        {
            this.results = results;
            this.count = count;
        }

        /// <summary>
        /// Gets the total result count.
        /// </summary>
        public int Count => this.count;

        /// <summary>
        /// Gets the results.
        /// </summary>
        public IEnumerable<T> Results => this.results;
    }
}