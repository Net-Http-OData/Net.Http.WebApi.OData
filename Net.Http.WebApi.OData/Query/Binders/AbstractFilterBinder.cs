// -----------------------------------------------------------------------
// <copyright file="AbstractFilterBinder.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Binders
{
    using System;
    using Net.Http.WebApi.OData.Query.Expressions;

    /// <summary>
    /// A base class for binding a filter expression tree.
    /// </summary>
    public abstract class AbstractFilterBinder
    {
        /// <summary>
        /// Binds the specified <see cref="BinaryOperatorNode"/>.
        /// </summary>
        /// <param name="binaryOperatorNode">The <see cref="BinaryOperatorNode"/> to bind.</param>
        protected abstract void Bind(BinaryOperatorNode binaryOperatorNode);

        /// <summary>
        /// Binds the specified <see cref="ConstantNode"/>.
        /// </summary>
        /// <param name="constantNode">The <see cref="ConstantNode"/> to bind.</param>
        protected abstract void Bind(ConstantNode constantNode);

        /// <summary>
        /// Binds the specified <see cref="QueryNode"/>.
        /// </summary>
        /// <param name="queryNode">The <see cref="QueryNode"/> to bind.</param>
        /// <exception cref="System.NotSupportedException">Thrown if the <see cref="QueryNodeKind"/> is not supported.</exception>
        protected void Bind(QueryNode queryNode)
        {
            var singleValueNode = queryNode as SingleValueNode;

            if (singleValueNode == null)
            {
                throw new NotSupportedException();
            }

            switch (queryNode.Kind)
            {
                case QueryNodeKind.BinaryOperator:
                    this.Bind((BinaryOperatorNode)queryNode);
                    break;

                case QueryNodeKind.Constant:
                    this.Bind((ConstantNode)queryNode);
                    break;

                case QueryNodeKind.SingleValueFunctionCall:
                    this.Bind((SingleValueFunctionCallNode)queryNode);
                    break;

                case QueryNodeKind.SingleValuePropertyAccess:
                    this.Bind((SingleValuePropertyAccessNode)queryNode);
                    break;

                case QueryNodeKind.UnaryOperator:
                    this.Bind((UnaryOperatorNode)queryNode);
                    break;

                default:
                    throw new NotSupportedException("Nodes of type '" + queryNode.Kind.ToString() + "' are not supported");
            }
        }

        /// <summary>
        /// Binds the specified <see cref="SingleValueFunctionCallNode"/>.
        /// </summary>
        /// <param name="singleValueFunctionCallNode">The <see cref="SingleValueFunctionCallNode"/> to bind.</param>
        protected abstract void Bind(SingleValueFunctionCallNode singleValueFunctionCallNode);

        /// <summary>
        /// Binds the specified <see cref="SingleValuePropertyAccessNode"/>.
        /// </summary>
        /// <param name="singleValuePropertyAccessNode">The <see cref="SingleValuePropertyAccessNode"/> to bind.</param>
        protected abstract void Bind(SingleValuePropertyAccessNode singleValuePropertyAccessNode);

        /// <summary>
        /// Binds the specified <see cref="UnaryOperatorNode"/>.
        /// </summary>
        /// <param name="unaryOperatorNode">The <see cref="UnaryOperatorNode"/> to bind.</param>
        protected abstract void Bind(UnaryOperatorNode unaryOperatorNode);
    }
}