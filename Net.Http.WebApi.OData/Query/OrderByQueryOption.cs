// -----------------------------------------------------------------------
// <copyright file="OrderByQueryOption.cs" company="MicroLite">
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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class containing deserialised values from the $order by query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class OrderByQueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OrderByQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        public OrderByQueryOption(string rawValue)
        {
            if (rawValue == null)
            {
                throw new ArgumentNullException("rawValue");
            }

            this.RawValue = rawValue;

            var pieces = rawValue.Split('=');
            var properties = pieces[1].Split(',').Select(raw => new OrderByProperty(raw)).ToArray();
            this.Properties = properties;
        }

        /// <summary>
        /// Gets the properties the query should be ordered by.
        /// </summary>
        public ICollection<OrderByProperty> Properties
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