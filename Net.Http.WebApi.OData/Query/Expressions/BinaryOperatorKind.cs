// -----------------------------------------------------------------------
// <copyright file="BinaryOperatorKind.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Expressions
{
    /// <summary>
    /// The kinds of binary operator.
    /// </summary>
    public enum BinaryOperatorKind
    {
        /// <summary>
        /// The binary kind is none.
        /// </summary>
        None = 0,

        /// <summary>
        /// The binary operator is or.
        /// </summary>
        Or = 1,

        /// <summary>
        /// The binary operator is and.
        /// </summary>
        And = 2,

        /// <summary>
        /// The binary operator is equal.
        /// </summary>
        Equal = 3,

        /// <summary>
        /// The binary operator is not equal.
        /// </summary>
        NotEqual = 4,

        /// <summary>
        /// The binary operator is greater than.
        /// </summary>
        GreaterThan = 5,

        /// <summary>
        /// The binary operator is greater than or equal.
        /// </summary>
        GreaterThanOrEqual = 6,

        /// <summary>
        /// The binary operator is less than.
        /// </summary>
        LessThan = 7,

        /// <summary>
        /// The binary operator is less than or equal.
        /// </summary>
        LessThanOrEqual = 8,
    }
}