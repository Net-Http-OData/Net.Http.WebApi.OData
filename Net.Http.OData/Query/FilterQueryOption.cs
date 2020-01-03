﻿// -----------------------------------------------------------------------
// <copyright file="FilterQueryOption.cs" company="Project Contributors">
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
namespace Net.Http.OData.Query
{
    using Net.Http.OData.Model;
    using Net.Http.OData.Query.Expressions;
    using Net.Http.OData.Query.Parsers;

    /// <summary>
    /// A class containing deserialised values from the $filter query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class FilterQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FilterQueryOption" /> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <param name="model">The model.</param>
        internal FilterQueryOption(string rawValue, EdmComplexType model)
            : base(rawValue)
        {
            this.Expression = FilterExpressionParser.Parse(rawValue, model);
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public QueryNode Expression { get; }
    }
}