// -----------------------------------------------------------------------
// <copyright file="AllowedFunctions.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Validation
{
    using System;

    /// <summary>
    /// An enumeration which represents the functions allowed in the $filter query option of an OData query.
    /// </summary>
    [Flags]
    public enum AllowedFunctions
    {
        /// <summary>
        /// Specifies that no functions are allowed.
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies that the 'StartsWith' function is allowed in the $filter query option.
        /// </summary>
        StartsWith = 1,

        /// <summary>
        /// Specifies that the 'EndsWith' function is allowed in the $filter query option.
        /// </summary>
        EndsWith = 2,

        /// <summary>
        /// Specifies that the 'SubstringOf' function is allowed in the $filter query option.
        /// </summary>
        SubstringOf = 3,

        /// <summary>
        /// Specifies that the 'ToLower' function is allowed in the $filter query option.
        /// </summary>
        ToLower = 4,

        /// <summary>
        /// Specifies that the 'ToUpper' function is allowed in the $filter query option.
        /// </summary>
        ToUpper = 5,

        /// <summary>
        /// Specifies that the 'Year' function is allowed in the $filter query option.
        /// </summary>
        Year = 6,

        /// <summary>
        /// Specifies that the 'Month' function is allowed in the $filter query option.
        /// </summary>
        Month = 7,

        /// <summary>
        /// Specifies that the 'Day' function is allowed in the $filter query option.
        /// </summary>
        Day = 8
    }
}