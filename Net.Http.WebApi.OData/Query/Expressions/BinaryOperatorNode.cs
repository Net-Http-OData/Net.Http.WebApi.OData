// -----------------------------------------------------------------------
// <copyright file="BinaryOperatorNode.cs" company="Project Contributors">
// Copyright 2012 - 2019 Project Contributors
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
    /// A QueryNode which represents a binary operator with a left and right branch.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Left} {OperatorKind} {Right}")]
    public sealed class BinaryOperatorNode : QueryNode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="BinaryOperatorNode"/> class.
        /// </summary>
        /// <param name="left">The left query node.</param>
        /// <param name="operatorKind">Kind of the operator.</param>
        /// <param name="right">The right query node.</param>
        internal BinaryOperatorNode(QueryNode left, BinaryOperatorKind operatorKind, QueryNode right)
        {
            this.Left = left;
            this.OperatorKind = operatorKind;
            this.Right = right;
        }

        /// <summary>
        /// Gets the kind of query node.
        /// </summary>
        public override QueryNodeKind Kind { get; } = QueryNodeKind.BinaryOperator;

        /// <summary>
        /// Gets the left query node.
        /// </summary>
        public QueryNode Left { get; internal set; }

        /// <summary>
        /// Gets the kind of the operator.
        /// </summary>
        public BinaryOperatorKind OperatorKind { get; }

        /// <summary>
        /// Gets the right query node.
        /// </summary>
        public QueryNode Right { get; internal set; }
    }
}