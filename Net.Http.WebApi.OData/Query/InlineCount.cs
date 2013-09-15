// -----------------------------------------------------------------------
// <copyright file="InlineCount.cs" company="Project Contributors">
// Copyright 2012-2013 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Query
{
    /// <summary>
    /// The valid inline count options.
    /// </summary>
    public enum InlineCount
    {
        /// <summary>
        /// The OData service MUST NOT include a count in the response.
        /// </summary>
        None = 0,

        /// <summary>
        /// The OData MUST include a count of the number of entities in the collection identified by the URI
        /// (after applying any $filter System Query Options present on the URI)
        /// </summary>
        AllPages = 1
    }
}