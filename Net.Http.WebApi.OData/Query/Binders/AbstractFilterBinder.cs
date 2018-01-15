// -----------------------------------------------------------------------
// <copyright file="AbstractFilterBinder.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Binders
{
    using System;
    using Expressions;

    /// <summary>
    /// A base class for binding the $filter query option.
    /// </summary>
    public abstract class AbstractFilterBinder
    {
        /// <summary>
        /// Binds the $filter value from the OData Query.
        /// </summary>
        /// <param name="filterQueryOption">The filter query option.</param>
        public void Bind(FilterQueryOption filterQueryOption)
        {
            if (filterQueryOption == null)
            {
                return;
            }

            this.Bind(filterQueryOption.Expression);
        }

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
            if (queryNode == null)
            {
                throw new ArgumentNullException(nameof(queryNode));
            }

            switch (queryNode.Kind)
            {
                case QueryNodeKind.BinaryOperator:
                    this.Bind((BinaryOperatorNode)queryNode);
                    break;

                case QueryNodeKind.Constant:
                    this.Bind((ConstantNode)queryNode);
                    break;

                case QueryNodeKind.FunctionCall:
                    this.Bind((FunctionCallNode)queryNode);
                    break;

                case QueryNodeKind.PropertyAccess:
                    this.Bind((PropertyAccessNode)queryNode);
                    break;

                case QueryNodeKind.UnaryOperator:
                    this.Bind((UnaryOperatorNode)queryNode);
                    break;

                default:
                    throw new NotSupportedException(Messages.UnsupportedNodeType.FormatWith(queryNode.Kind.ToString()));
            }
        }

        /// <summary>
        /// Binds the specified <see cref="FunctionCallNode"/>.
        /// </summary>
        /// <param name="functionCallNode">The <see cref="FunctionCallNode"/> to bind.</param>
        protected abstract void Bind(FunctionCallNode functionCallNode);

        /// <summary>
        /// Binds the specified <see cref="PropertyAccessNode"/>.
        /// </summary>
        /// <param name="propertyAccessNode">The <see cref="PropertyAccessNode"/> to bind.</param>
        protected abstract void Bind(PropertyAccessNode propertyAccessNode);

        /// <summary>
        /// Binds the specified <see cref="UnaryOperatorNode"/>.
        /// </summary>
        /// <param name="unaryOperatorNode">The <see cref="UnaryOperatorNode"/> to bind.</param>
        protected abstract void Bind(UnaryOperatorNode unaryOperatorNode);
    }
}