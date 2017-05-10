// -----------------------------------------------------------------------
// <copyright file="AllowedQueryOptions.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Validators
{
    using System;

    /// <summary>
    /// An enumeration which represents the query options allowed by an OData query.
    /// </summary>
    [Flags]
    public enum AllowedQueryOptions
    {
        /// <summary>
        /// Specifies that no query options are supported.
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies that the $select query option may be used.
        /// </summary>
        Select = 1,

        /// <summary>
        /// Specifies that the $filter query option may be used.
        /// </summary>
        Filter = 2,

        /// <summary>
        /// Specifies that the $expand query option may be used.
        /// </summary>
        Expand = 4,

        /// <summary>
        /// Specifies that the $skip query option may be used.
        /// </summary>
        Skip = 8,

        /// <summary>
        /// Specifies that the $top query option may be used.
        /// </summary>
        Top = 16,

        /// <summary>
        /// Specifies that the $skiptoken query option may be used.
        /// </summary>
        SkipToken = 32,

        /// <summary>
        /// Specifies that the $orderby query option may be used.
        /// </summary>
        OrderBy = 64,

        /// <summary>
        /// Specifies that the $count query option may be used.
        /// </summary>
        Count = 128,

        /// <summary>
        /// Specifies that the $format query option may be used.
        /// </summary>
        Format = 256,

        /// <summary>
        /// Specifies that the $search query option may be used.
        /// </summary>
        Search = 512,

        /// <summary>
        /// Specifies that all query options may be used.
        /// </summary>
        All = Select | Filter | Expand | Skip | Top | SkipToken | OrderBy | Count | Format | Search,
    }
}