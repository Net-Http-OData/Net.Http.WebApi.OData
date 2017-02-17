// -----------------------------------------------------------------------
// <copyright file="ExpandQueryOption.cs" company="Project Contributors">
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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A class containing deserialised values from the $expand query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class ExpandQueryOption : QueryOption
    {
        private static readonly char[] Comma = new[] { ',' };

        /// <summary>
        /// Initialises a new instance of the <see cref="ExpandQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        public ExpandQueryOption(string rawValue)
            : base(rawValue)
        {
            var equals = rawValue.IndexOf('=') + 1;
            var properties = rawValue.Substring(equals, rawValue.Length - equals).Split(Comma);

            this.Properties = properties;
        }

        /// <summary>
        /// Gets the properties to be included in the query.
        /// </summary>
        public IReadOnlyList<string> Properties
        {
            get;
        }
    }
}