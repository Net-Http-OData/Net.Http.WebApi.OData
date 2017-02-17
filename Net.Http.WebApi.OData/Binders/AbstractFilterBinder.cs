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
namespace Net.Http.WebApi.OData
{
    using System;
    using Net.Http.WebApi.OData.Query.Expressions;

    /// <summary>
    /// A base class for binding a filter expression tree.
    /// </summary>
    [Obsolete("This class has been replaced by Net.Http.WebApi.OData.Query.Binders.AbstractFilterBinder")]
    public abstract class AbstractFilterBinder : Query.Binders.AbstractFilterBinder
    {
        /// <summary>
        /// Binds the specified <see cref="BinaryOperatorNode" />.
        /// </summary>
        /// <param name="binaryOperatorNode">The <see cref="BinaryOperatorNode" /> to bind.</param>
        protected override void Bind(BinaryOperatorNode binaryOperatorNode) => this.BindBinaryOperatorNode(binaryOperatorNode);

        /// <summary>
        /// Binds the specified <see cref="ConstantNode" />.
        /// </summary>
        /// <param name="constantNode">The <see cref="ConstantNode" /> to bind.</param>
        protected override void Bind(ConstantNode constantNode) => this.BindConstantNode(constantNode);

        /// <summary>
        /// Binds the specified <see cref="SingleValueFunctionCallNode" />.
        /// </summary>
        /// <param name="singleValueFunctionCallNode">The <see cref="SingleValueFunctionCallNode" /> to bind.</param>
        protected override void Bind(SingleValueFunctionCallNode singleValueFunctionCallNode) => this.BindSingleValueFunctionCallNode(singleValueFunctionCallNode);

        /// <summary>
        /// Binds the specified <see cref="SingleValuePropertyAccessNode" />.
        /// </summary>
        /// <param name="singleValuePropertyAccessNode">The <see cref="SingleValuePropertyAccessNode" /> to bind.</param>
        protected override void Bind(SingleValuePropertyAccessNode singleValuePropertyAccessNode) => this.BindSingleValuePropertyAccessNode(singleValuePropertyAccessNode);

        /// <summary>
        /// Binds the specified <see cref="UnaryOperatorNode" />.
        /// </summary>
        /// <param name="unaryOperatorNode">The <see cref="UnaryOperatorNode" /> to bind.</param>
        protected override void Bind(UnaryOperatorNode unaryOperatorNode) => this.BindUnaryOperatorNode(unaryOperatorNode);

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