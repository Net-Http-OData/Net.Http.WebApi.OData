// -----------------------------------------------------------------------
// <copyright file="QueryNodeKind.cs" company="Project Contributors">
// Copyright 2012 - 2016 Project Contributors
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
    /// Gets the kinds of query node
    /// </summary>
    public enum QueryNodeKind
    {
        /// <summary>
        /// The query node is not specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// The query node is a binary operator query node.
        /// </summary>
        BinaryOperator = 1,

        /// <summary>
        /// The single value property access query node.
        /// </summary>
        SingleValuePropertyAccess = 2,

        /// <summary>
        /// The constant value query node.
        /// </summary>
        Constant = 3,

        /// <summary>
        /// The single value function call query node.
        /// </summary>
        SingleValueFunctionCall = 4,

        /// <summary>
        /// The query node is a unary operator query node.
        /// </summary>
        UnaryOperator = 5
    }
}