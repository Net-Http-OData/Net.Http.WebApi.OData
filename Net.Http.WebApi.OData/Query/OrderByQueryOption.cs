// -----------------------------------------------------------------------
// <copyright file="OrderByQueryOption.cs" company="Project Contributors">
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
    using System.Linq;

    /// <summary>
    /// A class containing deserialised values from the $order by query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class OrderByQueryOption
    {
        private static readonly char[] Comma = new[] { ',' };

        /// <summary>
        /// Initialises a new instance of the <see cref="OrderByQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        public OrderByQueryOption(string rawValue)
        {
            if (rawValue == null)
            {
                throw new ArgumentNullException(nameof(rawValue));
            }

            this.RawValue = rawValue;

            var equals = rawValue.IndexOf('=') + 1;
            var properties = rawValue.Substring(equals, rawValue.Length - equals);

            if (properties.Contains(','))
            {
                this.Properties = properties.Split(Comma)
                    .Select(raw => new OrderByProperty(raw))
                    .ToArray();
            }
            else
            {
                this.Properties = new[] { new OrderByProperty(properties) };
            }
        }

        /// <summary>
        /// Gets the properties the query should be ordered by.
        /// </summary>
        public IList<OrderByProperty> Properties
        {
            get;
        }

        /// <summary>
        /// Gets the raw request value.
        /// </summary>
        public string RawValue
        {
            get;
        }
    }
}