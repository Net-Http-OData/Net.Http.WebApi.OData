// -----------------------------------------------------------------------
// <copyright file="ConstantNode.cs" company="Project Contributors">
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
    /// A QueryNode which represents a constant value.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Value}")]
    public sealed class ConstantNode : SingleValueNode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        public ConstantNode(string literalText, object value)
        {
            this.LiteralText = literalText;
            this.Value = value;
        }

        /// <summary>
        /// Gets the kind of query node.
        /// </summary>
        public override QueryNodeKind Kind
        {
            get
            {
                return QueryNodeKind.Constant;
            }
        }

        /// <summary>
        /// Gets the literal text if the constant value.
        /// </summary>
        public string LiteralText
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the constant value as an object.
        /// </summary>
        public object Value
        {
            get;
            private set;
        }
    }
}