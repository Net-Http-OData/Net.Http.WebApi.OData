// -----------------------------------------------------------------------
// <copyright file="SingleValueFunctionCallNode.cs" company="Project Contributors">
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
    using System.Collections.Generic;

    /// <summary>
    /// A QueryNode which represents a function call.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public sealed class SingleValueFunctionCallNode : SingleValueNode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SingleValueFunctionCallNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        public SingleValueFunctionCallNode(string name, IList<QueryNode> arguments)
        {
            this.Name = name;
            this.Arguments = arguments;
        }

        /// <summary>
        /// Gets the arguments for the function call.
        /// </summary>
        public IList<QueryNode> Arguments
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
                return QueryNodeKind.SingleValueFunctionCall;
            }
        }

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }
    }
}