// -----------------------------------------------------------------------
// <copyright file="AllowedQueryOptions.cs" company="MicroLite">
// Copyright 2012-2013 Trevor Pilley
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Query.Validation
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
        /// Specifies that the $skip token query option may be used.
        /// </summary>
        SkipToken = 32,

        /// <summary>
        /// Specifies that the $order by query option may be used.
        /// </summary>
        OrderBy = 64,

        /// <summary>
        /// Specifies that the $inline count query option may be used.
        /// </summary>
        InlineCount = 128,

        /// <summary>
        /// Specifies that the $format query option may be used.
        /// </summary>
        Format = 256
    }
}