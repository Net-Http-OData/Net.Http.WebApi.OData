// -----------------------------------------------------------------------
// <copyright file="FilterQueryOption.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query
{
    using Expressions;
    using Parsers;

    /// <summary>
    /// A class containing deserialised values from the $filter query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class FilterQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FilterQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        internal FilterQueryOption(string rawValue)
            : base(rawValue)
        {
            this.Expression = FilterExpressionParser.Parse(rawValue);
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public QueryNode Expression
        {
            get;
        }
    }
}