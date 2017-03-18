// -----------------------------------------------------------------------
// <copyright file="ConstantNode.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Expressions
{
    using System;
    using Model;

    /// <summary>
    /// A QueryNode which represents a constant value.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{LiteralText}")]
    public sealed class ConstantNode : QueryNode
    {
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
        /// Gets the <see cref="EdmType"/> of the value.
        /// </summary>
        public EdmType EdmType
        {
            get;
        }

        /// <summary>
        /// Gets the kind of query node.
        /// </summary>
        public override QueryNodeKind Kind { get; } = QueryNodeKind.Constant;

        /// <summary>
        /// Gets the literal text if the constant value.
        /// </summary>
        public string LiteralText
        {
            get;
        }

        /// <summary>
        /// Gets the constant value as an object.
        /// </summary>
        public object Value
        {
            get;
        }

        /// <summary>
        /// Gets the ConstantNode which represents a value of false.
        /// </summary>
        internal static ConstantNode False { get; } = new ConstantNode(EdmPrimitiveType.Boolean, "false", false);

        /// <summary>
        /// Gets the ConstantNode which represents a 32bit integer value of 0.
        /// </summary>
        internal static ConstantNode Int32Zero { get; } = new ConstantNode(EdmPrimitiveType.Int32, "0", 0);

        /// <summary>
        /// Gets the ConstantNode which represents a 64bit integer value of 0.
        /// </summary>
        internal static ConstantNode Int64Zero { get; } = new ConstantNode(EdmPrimitiveType.Int64, "0L", 0L);

        /// <summary>
        /// Gets the ConstantNode which represents a value of null.
        /// </summary>
        internal static ConstantNode Null { get; } = new ConstantNode(null, "null", null);

        /// <summary>
        /// Gets the ConstantNode which represents a value of true.
        /// </summary>
        internal static ConstantNode True { get; } = new ConstantNode(EdmPrimitiveType.Boolean, "true", true);

        /// <summary>
        /// Gets a ConstantNode which represents a Date value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a Date value.</returns>
        internal static ConstantNode Date(string literalText, DateTime value) => new ConstantNode(EdmPrimitiveType.Date, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a DateTimeOffset value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a DateTimeOffset value.</returns>
        internal static ConstantNode DateTimeOffset(string literalText, DateTimeOffset value) => new ConstantNode(EdmPrimitiveType.DateTimeOffset, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a decimal value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a decimal value.</returns>
        internal static ConstantNode Decimal(string literalText, decimal value) => new ConstantNode(EdmPrimitiveType.Decimal, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a double value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a double value.</returns>
        internal static ConstantNode Double(string literalText, double value) => new ConstantNode(EdmPrimitiveType.Double, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a duration value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a duration value.</returns>
        internal static ConstantNode Duration(string literalText, TimeSpan value) => new ConstantNode(EdmPrimitiveType.Duration, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a Guid value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a Guid value.</returns>
        internal static ConstantNode Guid(string literalText, Guid value) => new ConstantNode(EdmPrimitiveType.Guid, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a 32 bit signed integer value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a 32 bit signed integer value.</returns>
        internal static ConstantNode Int32(string literalText, int value) => new ConstantNode(EdmPrimitiveType.Int32, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a 64 bit signed integer value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a 64 bit signed integer value.</returns>
        internal static ConstantNode Int64(string literalText, long value) => new ConstantNode(EdmPrimitiveType.Int64, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a float value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a float value.</returns>
        internal static ConstantNode Single(string literalText, float value) => new ConstantNode(EdmPrimitiveType.Single, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a string value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a string value.</returns>
        internal static ConstantNode String(string literalText, string value) => new ConstantNode(EdmPrimitiveType.String, literalText, value);

        /// <summary>
        /// Gets a ConstantNode which represents a time value.
        /// </summary>
        /// <param name="literalText">The literal text.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConstantNode representing a time value.</returns>
        internal static ConstantNode Time(string literalText, TimeSpan value) => new ConstantNode(EdmPrimitiveType.TimeOfDay, literalText, value);
    }
}