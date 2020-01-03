﻿// -----------------------------------------------------------------------
// <copyright file="UnaryOperatorNode.cs" company="Project Contributors">
// Copyright 2012 - 2020 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.OData.Query.Expressions
{
    /// <summary>
    /// A QueryNode which represents a unary operator.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{OperatorKind} {Operand}")]
    public sealed class UnaryOperatorNode : QueryNode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UnaryOperatorNode"/> class.
        /// </summary>
        /// <param name="operand">The operand of the unary operator.</param>
        /// <param name="operatorKind">Kind of the operator.</param>
        internal UnaryOperatorNode(QueryNode operand, UnaryOperatorKind operatorKind)
        {
            this.Operand = operand;
            this.OperatorKind = operatorKind;
        }

        /// <summary>
        /// Gets the kind of query node.
        /// </summary>
        public override QueryNodeKind Kind { get; } = QueryNodeKind.UnaryOperator;

        /// <summary>
        /// Gets the operand of the unary operator.
        /// </summary>
        public QueryNode Operand { get; }

        /// <summary>
        /// Gets the kind of the operator.
        /// </summary>
        public UnaryOperatorKind OperatorKind { get; }
    }
}