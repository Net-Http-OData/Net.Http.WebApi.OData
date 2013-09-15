// -----------------------------------------------------------------------
// <copyright file="FilterQueryOption.cs" company="MicroLite">
// Copyright 2012-2013 Trevor Pilley
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
    using System;
    using Net.Http.WebApi.OData.Query.Expression;

    /// <summary>
    /// A class containing deserialised values from the $filter query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class FilterQueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FilterQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        public FilterQueryOption(string rawValue)
        {
            if (rawValue == null)
            {
                throw new ArgumentNullException("rawValue");
            }

            this.RawValue = rawValue;

            var equals = rawValue.IndexOf('=') + 1;
            this.Expression = FilterExpressionParser.Parse(rawValue.Substring(equals, rawValue.Length - equals));
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public QueryNode Expression
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw request value.
        /// </summary>
        public string RawValue
        {
            get;
            private set;
        }
    }
}