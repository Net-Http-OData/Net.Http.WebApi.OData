// -----------------------------------------------------------------------
// <copyright file="AllowedLogicalOperators.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query
{
    using System;

    /// <summary>
    /// An enumeration which represents the logical operators allowed in the $filter query option of an OData query.
    /// </summary>
    [Flags]
    public enum AllowedLogicalOperators
    {
        /// <summary>
        /// Specifies that no logical operators are allowed in the $filter query option.
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies that the 'eq' logical operator is allowed in the $filter query option.
        /// </summary>
        Equal = 1,

        /// <summary>
        /// Specifies that the 'ne' logical operator is allowed in the $filter query option.
        /// </summary>
        NotEqual = 2,

        /// <summary>
        /// Specifies that the 'gt' logical operator is allowed in the $filter query option.
        /// </summary>
        GreaterThan = 4,

        /// <summary>
        /// Specifies that the 'ge' logical operator is allowed in the $filter query option.
        /// </summary>
        GreaterThanOrEqual = 8,

        /// <summary>
        /// Specifies that the 'lt' logical operator is allowed in the $filter query option.
        /// </summary>
        LessThan = 16,

        /// <summary>
        /// Specifies that the 'le' logical operator is allowed in the $filter query option.
        /// </summary>
        LessThanOrEqual = 32,

        /// <summary>
        /// Specifies that the 'and' logical operator is allowed in the $filter query option.
        /// </summary>
        And = 64,

        /// <summary>
        /// Specifies that the 'or' logical operator is allowed in the $filter query option.
        /// </summary>
        Or = 128,

        /// <summary>
        /// Specifies that all logical operators are allowed in the $filter query option.
        /// </summary>
        All = Equal | NotEqual | GreaterThan | GreaterThanOrEqual | LessThan | LessThanOrEqual | And | Or
    }
}