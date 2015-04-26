// -----------------------------------------------------------------------
// <copyright file="ConstantNode.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Expressions
{
    using System;

    /// <summary>
    /// A QueryNode which represents a constant value.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Value}")]
    public sealed class ConstantNode : SingleValueNode
    {
        private static ConstantNode nullConstantNode = new ConstantNode(EdmType.Null, "null", null);

        /// <summary>
        /// Initialises a new instance of the <see cref="ConstantNode" /> class.
        /// </summary>
        /// <param name="edmType">The <see cref="EdmType"/> of the value.</param>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        private ConstantNode(EdmType edmType, string literalText, object value)
        {
            this.EdmType = edmType;
            this.LiteralText = literalText;
            this.Value = value;
        }

        /// <summary>
        /// Gets the ConstantNode which represents a null value.
        /// </summary>
        public static ConstantNode Null
        {
            get
            {
                return nullConstantNode;
            }
        }

        /// <summary>
        /// Gets the <see cref="EdmType"/> of the value.
        /// </summary>
        public EdmType EdmType
        {
            get;
            private set;
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

        /// <summary>
        /// Gets a ConstantNode which represents a boolean value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a boolean value.</returns>
        public static ConstantNode Boolean(string literalText, bool value)
        {
            return new ConstantNode(EdmType.Boolean, literalText, value);
        }

        /// <summary>
        /// Gets a ConstantNode which represents a DateTime value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a DateTime value.</returns>
        public static ConstantNode DateTime(string literalText, DateTime value)
        {
            return new ConstantNode(EdmType.DateTime, literalText, value);
        }

        /// <summary>
        /// Gets a ConstantNode which represents a DateTimeOffset value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a DateTimeOffset value.</returns>
        public static ConstantNode DateTimeOffset(string literalText, DateTimeOffset value)
        {
            return new ConstantNode(EdmType.DateTimeOffset, literalText, value);
        }

        /// <summary>
        /// Gets a ConstantNode which represents a decimal value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a decimal value.</returns>
        public static ConstantNode Decimal(string literalText, decimal value)
        {
            return new ConstantNode(EdmType.Decimal, literalText, value);
        }

        /// <summary>
        /// Gets a ConstantNode which represents a double value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a double value.</returns>
        public static ConstantNode Double(string literalText, double value)
        {
            return new ConstantNode(EdmType.Double, literalText, value);
        }

        /// <summary>
        /// Gets a ConstantNode which represents a Guid value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a Guid value.</returns>
        public static ConstantNode Guid(string literalText, Guid value)
        {
            return new ConstantNode(EdmType.Guid, literalText, value);
        }

        /// <summary>
        /// Gets a ConstantNode which represents a 32 bit signed integer value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a 32 bit signed integer value.</returns>
        public static ConstantNode Int32(string literalText, int value)
        {
            return new ConstantNode(EdmType.Int32, literalText, value);
        }

        /// <summary>
        /// Gets a ConstantNode which represents a float value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a float value.</returns>
        public static ConstantNode Single(string literalText, float value)
        {
            return new ConstantNode(EdmType.Single, literalText, value);
        }

        /// <summary>
        /// Gets a ConstantNode which represents a string value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a string value.</returns>
        public static ConstantNode String(string literalText, string value)
        {
            return new ConstantNode(EdmType.String, literalText, value);
        }

        /// <summary>
        /// Gets a ConstantNode which represents a time value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a time value.</returns>
        public static ConstantNode Time(string literalText, TimeSpan value)
        {
            return new ConstantNode(EdmType.Time, literalText, value);
        }
    }
}