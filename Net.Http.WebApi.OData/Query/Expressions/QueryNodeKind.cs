// -----------------------------------------------------------------------
// <copyright file="QueryNodeKind.cs" company="Project Contributors">
// Copyright 2012 - 2018 Project Contributors
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
    /// Gets the kinds of query node.
    /// </summary>
    public enum QueryNodeKind
    {
        /// <summary>
        /// The query node kind is not specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// The query node is a binary operator query node.
        /// </summary>
        BinaryOperator = 1,

        /// <summary>
        /// The query node is a property access query node.
        /// </summary>
        PropertyAccess = 2,

        /// <summary>
        /// The query node is a constant value query node.
        /// </summary>
        Constant = 3,

        /// <summary>
        /// The query node is a function call query node.
        /// </summary>
        FunctionCall = 4,

        /// <summary>
        /// The query node is a unary operator query node.
        /// </summary>
        UnaryOperator = 5,
    }
}