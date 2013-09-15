// -----------------------------------------------------------------------
// <copyright file="SingleValuePropertyAccessNode.cs" company="Project Contributors">
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
    /// A QueryNode which represents a property.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{PropertyName}")]
    public sealed class SingleValuePropertyAccessNode : SingleValueNode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SingleValuePropertyAccessNode"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public SingleValuePropertyAccessNode(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets the kind of query node.
        /// </summary>
        public override QueryNodeKind Kind
        {
            get
            {
                return QueryNodeKind.SingleValuePropertyAccess;
            }
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropertyName
        {
            get;
            private set;
        }
    }
}