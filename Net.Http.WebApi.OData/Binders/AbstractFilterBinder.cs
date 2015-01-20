// -----------------------------------------------------------------------
// <copyright file="AbstractFilterBinder.cs" company="Project Contributors">
// Copyright 2012 - 2015 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData
{
    using System;
    using Net.Http.WebApi.OData.Query.Expressions;

    /// <summary>
    /// A base class for binding a filter expression tree.
    /// </summary>
    public abstract class AbstractFilterBinder
    {
        /// <summary>
        /// Binds the specified <see cref="QueryNode"/>.
        /// </summary>
        /// <param name="node">The <see cref="QueryNode"/> to bind.</param>
        /// <exception cref="System.NotSupportedException">Thrown if the node is not supported.</exception>
        protected void Bind(QueryNode node)
        {
            var singleValueNode = node as SingleValueNode;

            if (singleValueNode == null)
            {
                throw new NotSupportedException();
            }

            switch (node.Kind)
            {
                case QueryNodeKind.BinaryOperator:
                    this.BindBinaryOperatorNode((BinaryOperatorNode)node);
                    break;

                case QueryNodeKind.Constant:
                    this.BindConstantNode((ConstantNode)node);
                    break;

                case QueryNodeKind.SingleValueFunctionCall:
                    this.BindSingleValueFunctionCallNode((SingleValueFunctionCallNode)node);
                    break;

                case QueryNodeKind.SingleValuePropertyAccess:
                    this.BindSingleValuePropertyAccessNode((SingleValuePropertyAccessNode)node);
                    break;

                case QueryNodeKind.UnaryOperator:
                    this.BindUnaryOperatorNode((UnaryOperatorNode)node);
                    break;

                default:
                    throw new NotSupportedException("Nodes of type '" + node.Kind + "' are not supported");
            }
        }

        /// <summary>
        /// Binds the specified <see cref="BinaryOperatorNode"/>.
        /// </summary>
        /// <param name="binaryOperatorNode">The <see cref="BinaryOperatorNode"/> to bind.</param>
        protected abstract void BindBinaryOperatorNode(BinaryOperatorNode binaryOperatorNode);

        /// <summary>
        /// Binds the specified <see cref="ConstantNode"/>.
        /// </summary>
        /// <param name="constantNode">The <see cref="ConstantNode"/> to bind.</param>
        protected abstract void BindConstantNode(ConstantNode constantNode);

        /// <summary>
        /// Binds the specified <see cref="SingleValueFunctionCallNode"/>.
        /// </summary>
        /// <param name="singleValueFunctionCallNode">The <see cref="SingleValueFunctionCallNode"/> to bind.</param>
        protected abstract void BindSingleValueFunctionCallNode(SingleValueFunctionCallNode singleValueFunctionCallNode);

        /// <summary>
        /// Binds the specified <see cref="SingleValuePropertyAccessNode"/>.
        /// </summary>
        /// <param name="singleValuePropertyAccessNode">The <see cref="SingleValuePropertyAccessNode"/> to bind.</param>
        protected abstract void BindSingleValuePropertyAccessNode(SingleValuePropertyAccessNode singleValuePropertyAccessNode);

        /// <summary>
        /// Binds the specified <see cref="UnaryOperatorNode"/>.
        /// </summary>
        /// <param name="unaryOperatorNode">The <see cref="UnaryOperatorNode"/> to bind.</param>
        protected abstract void BindUnaryOperatorNode(UnaryOperatorNode unaryOperatorNode);
    }
}