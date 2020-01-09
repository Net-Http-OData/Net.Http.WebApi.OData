﻿// -----------------------------------------------------------------------
// <copyright file="PropertyAccessNode.cs" company="Project Contributors">
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
    /// A QueryNode which represents a property.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{PropertyPath}")]
    public sealed class PropertyAccessNode : QueryNode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PropertyAccessNode"/> class.
        /// </summary>
        /// <param name="property">The property being referenced in the query.</param>
        internal PropertyAccessNode(PropertyPathSegment propertyPathSegment)
        {
            this.PropertyPath = propertyPathSegment;
        }

        /// <summary>
        /// Gets the kind of query node.
        /// </summary>
        public override QueryNodeKind Kind { get; } = QueryNodeKind.PropertyAccess;

        /// <summary>
        /// Gets the property path being referenced in the query.
        /// </summary>
        public PropertyPathSegment PropertyPath { get; }
    }
}