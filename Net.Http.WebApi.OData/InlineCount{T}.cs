// -----------------------------------------------------------------------
// <copyright file="InlineCount{T}.cs" company="Project Contributors">
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
    [System.Runtime.Serialization.DataContract]
    public sealed class InlineCount<T>
    {
        private readonly int count;
        private readonly IList<T> results;

        /// <summary>
        /// Initialises a new instance of the <see cref="InlineCount{T}"/> class.
        /// </summary>
        /// <param name="results">The items to be returned.</param>
        /// <param name="count">The total result count.</param>
        public InlineCount(IList<T> results, int count)
        {
            this.results = results;
            this.count = count;
        }

        /// <summary>
        /// Gets the total result count.
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "__count", Order = 0)]
        public int Count
        {
            get
            {
                return this.count;
            }
        }

        /// <summary>
        /// Gets the results.
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "results", Order = 1)]
        public IEnumerable<T> Results
        {
            get
            {
                return this.results;
            }
        }
    }
}