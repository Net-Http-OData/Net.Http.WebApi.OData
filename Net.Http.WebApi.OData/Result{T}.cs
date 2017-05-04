﻿// -----------------------------------------------------------------------
// <copyright file="Result{T}.cs" company="Project Contributors">
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
    /// A class which is used to return results.
    /// </summary>
    /// <typeparam name="T">The type of item contained in the collection.</typeparam>
    [System.Runtime.Serialization.DataContract]
    public class Result<T>
    {
        [System.Runtime.Serialization.DataMember(Name = "value", Order = 0)]
        private readonly IList<T> results;

        /// <summary>
        /// Initialises a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="results">The items to be returned.</param>
        public Result(IList<T> results)
        {
            this.results = results;
        }

        /// <summary>
        /// Gets the results.
        /// </summary>
        public IEnumerable<T> Results => this.results;
    }
}