// -----------------------------------------------------------------------
// <copyright file="OrderByQueryOption.cs" company="Project Contributors">
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
    using System.Collections.Generic;
    using System.Linq;
    using Net.Http.OData.Model;

    /// <summary>
    /// A class containing deserialised values from the $orderby query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class OrderByQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OrderByQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <param name="model">The model.</param>
        internal OrderByQueryOption(string rawValue, EdmComplexType model)
            : base(rawValue)
        {
            var equals = rawValue.IndexOf('=') + 1;
            var properties = rawValue.Substring(equals, rawValue.Length - equals);

            if (properties.IndexOf(',') > 0)
            {
                this.Properties = properties.Split(SplitCharacter.Comma)
                    .Select(raw => new OrderByProperty(raw, model))
                    .ToArray();
            }
            else
            {
                this.Properties = new[] { new OrderByProperty(properties, model) };
            }
        }

        /// <summary>
        /// Gets the properties the query should be ordered by.
        /// </summary>
        public IReadOnlyList<OrderByProperty> Properties { get; }
    }
}