// -----------------------------------------------------------------------
// <copyright file="BinaryOperatorNode.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Expression
{
    /// <summary>
    /// A QueryNode which represents a binary operator with a left and right branch.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Left} {OperatorKind} {Right}")]
    public sealed class BinaryOperatorNode : SingleValueNode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="BinaryOperatorNode"/> class.
        /// </summary>
        public BinaryOperatorNode()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="BinaryOperatorNode"/> class.
        /// </summary>
        /// <param name="left">The left query node.</param>
        /// <param name="operatorKind">Kind of the operator.</param>
        /// <param name="right">The right query node.</param>
        public BinaryOperatorNode(SingleValueNode left, BinaryOperatorKind operatorKind, SingleValueNode right)
        {
            this.Left = left;
            this.OperatorKind = operatorKind;
            this.Right = right;
        }

        /// <summary>
        /// Gets the kind of query node.
        /// </summary>
        public override QueryNodeKind Kind
        {
            get
            {
                return QueryNodeKind.BinaryOperator;
            }
        }

        /// <summary>
        /// Gets or sets the left query node.
        /// </summary>
        public SingleValueNode Left
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the kind of the operator.
        /// </summary>
        public BinaryOperatorKind OperatorKind
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the right query node.
        /// </summary>
        public SingleValueNode Right
        {
            get;
            set;
        }
    }
}